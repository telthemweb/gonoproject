using Application.Data.Common;
using Application.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Application.Data
{
    public class Nationality : BaseEntity, IAuditable
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
