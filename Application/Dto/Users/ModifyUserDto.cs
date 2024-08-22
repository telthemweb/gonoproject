using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Users
{
    public class ModifyUserDto
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Title is requried")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        public string Phonenumber { get; set; }

        [Required(ErrorMessage = "Gender is requried")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Status is requried")]
        public string Status { get; set; }

     
    }
}
