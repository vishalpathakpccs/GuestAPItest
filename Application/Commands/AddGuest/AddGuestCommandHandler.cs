
using Core.Entities;
using Core.Interfaces;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.AddGuest
{
    public class AddGuestCommandHandler : IRequestHandler<AddGuestCommand, (ValidationResult, Guest)>
    {
        private readonly IGuestRepository _repository;

        public AddGuestCommandHandler(IGuestRepository repository)
        {
            _repository = repository;
        }

        public async Task<(ValidationResult, Guest)> Handle(AddGuestCommand request, CancellationToken cancellationToken)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(request);
            if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
            {
                return (new ValidationResult(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage))),null);
            }

            var guest = new Guest
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                BirthDate = request.BirthDate,
                Email = request.Email,
                PhoneNumbers = request.PhoneNumbers
            };

            await _repository.AddAsync(guest);
            return ( new ValidationResult("") , guest);
        }
    }
}
