using Application.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Users
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }


        public string? Gender { get; set; }

        public string Status { get; set; }
        public DateTime PasswordExpireDate { get; set; }
    }
}
