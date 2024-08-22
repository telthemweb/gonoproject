using Application.Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Systempermissions
{
    public class ModifySystempermissionDto:BaseDto
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "System submodule is required")]
        public int SystemsubmoduleId { get; set; }
    }
}
