
using Core.Entities;
using Core.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.AddGuest
{
    public class AddGuestCommand : IRequest<(ValidationResult, Guest)>
    {
        [Required]
        public Title Title { get; set; }

        [Required(ErrorMessage = "Firstname is required.")]
        [StringLength(50, ErrorMessage = "Firstname cannot be longer than 50 characters.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        [StringLength(50, ErrorMessage = "Lastname cannot be longer than 50 characters.")]
        public string Lastname { get; set; }

        public DateTime BirthDate { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "At least one phone number is required")]
        [MinLength(1, ErrorMessage = "At least one phone number must be provided.")]
        public List<string> PhoneNumbers { get; set; }
    }
}
