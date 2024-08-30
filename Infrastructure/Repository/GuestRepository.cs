
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class GuestRepository : IGuestRepository
    {
        private readonly List<Guest> _guests = new List<Guest>();
        private readonly IMemoryCache _cache;

        public GuestRepository(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<Guest> GetByIdAsync(Guid id)
        {
            _cache.TryGetValue(id, out Guest guest);
            if (guest == null)
                return await Task.FromResult(_guests.SingleOrDefault(g => g.Id == id));
            else
                return guest;
        }

        public async Task<IReadOnlyList<Guest>> ListAllAsync()
        {
            var guests = _cache.GetOrCreate("AllGuests", entry => new List<Guest>());
            if (guests == null)
                return await Task.FromResult(_guests.AsReadOnly());
            else
                return guests;
        }

        public async Task<Guest> AddAsync(Guest entity)
        {
            var guests = _cache.GetOrCreate("AllGuests", entry => new List<Guest>());
            guests.Add(entity);
            _guests.Add(entity);
            _cache.Set(entity.Id, entity);
            return await Task.FromResult(entity);
        }

        public async Task<bool> AddPhoneAsync(Guid id, string phoneNumber)
        {
            _cache.TryGetValue(id, out Guest guest);
            if (guest == null)
                 guest = await GetByIdAsync(id);
            var newguest = guest;
            if (guest != null)
            {
                if (newguest.PhoneNumbers.Contains(phoneNumber))
                    return await Task.FromResult(false);
                newguest.PhoneNumbers.Add(phoneNumber);
                _guests.Remove(guest);
                _guests.Add(newguest);
                _cache.Set(id, newguest);
                return await Task.FromResult(true);
            }
            else
                return await Task.FromResult(false);
        }


    }
}
