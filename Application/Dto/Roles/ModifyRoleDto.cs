using Application.Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Roles
{
    public class ModifyRoleDto
    {
        public string? Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

    }
}
