using Application.Dto.Common;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class GenderModifyDto:BaseDto
    {
        [Required]
        public string Name { get; set; }
    }
}
