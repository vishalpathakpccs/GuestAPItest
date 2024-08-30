
using Core.Entities;
using Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.GetGuestById
{
    public class GetGuestByIdQueryHandler : IRequestHandler<GetGuestByIdQuery, Guest>
    {
        private readonly IGuestRepository _repository;

        public GetGuestByIdQueryHandler(IGuestRepository repository)
        {
            _repository = repository;
        }

        public Task<Guest> Handle(GetGuestByIdQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetByIdAsync(request.Id);
        }
    }
}
