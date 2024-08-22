using Application.Data.Common;
using Application.Data.Interfaces;

namespace Application.Data
{
    public class City : BaseEntity, IAuditable
    {
        public int ProvinceId { get; set; }

        public string? Name { get; set; }

        public Province? province { get; set; }
    }
}
