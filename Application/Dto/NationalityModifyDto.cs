using Application.Dto.Common;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class NationalityModifyDto:BaseDto
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
