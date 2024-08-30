using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Interfaces;

namespace Application.Commands.AddPhone
{
    public class AddPhoneCommandHandler : IRequestHandler<AddPhoneCommand, ValidationResult>
    {
        private readonly IGuestRepository _repository;

        public AddPhoneCommandHandler(IGuestRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> Handle(AddPhoneCommand request, CancellationToken cancellationToken)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(request);
            if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
            {
                return (new ValidationResult(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage))));
            }
            if (await _repository.AddPhoneAsync(request.Id, request.PhoneNumber))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Phone number already exists or guest not found.");
        }
    }
}
