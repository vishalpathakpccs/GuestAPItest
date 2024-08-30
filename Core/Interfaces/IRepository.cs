using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGuestRepository
    {
        Task<Guest> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Guest>> ListAllAsync();
        Task<Guest> AddAsync(Guest entity);
        Task<bool> AddPhoneAsync(Guid id, string phoneNumber);
    }
}
