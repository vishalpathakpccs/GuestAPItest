
using Core.Entities;
using MediatR;
using System;

namespace Application.Queries.GetGuestById
{
    public class GetGuestByIdQuery : IRequest<Guest>
    {
        public Guid Id { get; set; }
    }
}
