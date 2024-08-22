using Application.Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Systemsubmodules
{
    public class ModifySystemsubmoduleDto : BaseDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Icon is required")]
        public string Icon { get; set; }


        [Required(ErrorMessage = "Module url is required")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Access name is required")]
        public string AccessName { get; set; }

        [Required(ErrorMessage ="System module is required")]
        public int SystemmoduleId { get; set; }
    }
}
