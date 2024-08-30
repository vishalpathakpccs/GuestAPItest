using Core.Entities;
using MediatR;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetGuestById
{
    public class GetAllGuestsQueryHandler : IRequestHandler<GetAllGuestsQuery, IEnumerable<Guest>>
    {
        private readonly IGuestRepository _repository;

        public GetAllGuestsQueryHandler(IGuestRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Guest>> Handle(GetAllGuestsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ListAllAsync();
        }
    }
}
