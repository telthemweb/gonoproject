using Application.Dto.Common;

namespace Application.Dto
{
    public class CityModifyDto:BaseDto
    {
        public int ProvinceId { get; set; }
        public string? Name { get; set; }
    }
}
