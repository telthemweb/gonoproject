using Application.Dto.Common;

namespace Application.Dto
{
    public class VisitorNumberModifyDto:BaseDto
    {
        public string? Prefix { get; set; }
        public int? LastNumber { get; set; }
        public int? Year { get; set; }
    }
}
