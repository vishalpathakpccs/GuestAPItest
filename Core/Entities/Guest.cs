using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Guest
    {
        public Guid Id { get; set; }

        [Required]
        public Title Title { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public DateTime BirthDate { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "At least one phone number is required")]
        [MinLength(1, ErrorMessage = "At least one phone number must be provided.")]
        public List<string> PhoneNumbers { get; set; } = new List<string>();
    }
}
