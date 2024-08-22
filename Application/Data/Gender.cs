using Application.Data.Common;
using Application.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Application.Data
{
    public class Gender : BaseEntity, IAuditable
    {
        [Required]
        public string Name { get; set; }
    }
}