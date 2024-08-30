using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.AddPhone
{
    public class AddPhoneCommand : IRequest<ValidationResult>
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "phone number is required")]
        public string PhoneNumber { get; set; }
    }
    
}
