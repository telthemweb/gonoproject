using Application.Dto.Common;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class TitleModifyDto : BaseDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
    }
}
