using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Identity
{
    public class PasswordResetRequestDto
    {

        [Required(ErrorMessage ="Token is required")]
        public string Token { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "New password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        public string ConfirmPassword { get; set; }
    }
}
