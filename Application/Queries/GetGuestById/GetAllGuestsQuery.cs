using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetGuestById
{
    public class GetAllGuestsQuery : IRequest<IEnumerable<Guest>>
    {
    }
}
