using Application.Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Systemmodules
{
    public class ModifySystemmoduleDto:BaseDto
    {

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Icon is required")]
        public string Icon { get; set; }

        [Required(ErrorMessage = "Access Name is required")]
        public string AccessName { get; set; }
    }
}
