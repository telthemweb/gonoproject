using Application.Data.Common;
using Application.Data.Interfaces;

namespace Application.Data
{
    public class VisitorNumber : BaseEntity, IAuditable
    {
        public string? Prefix { get; set; }
        public int? LastNumber { get; set; }
        public int? Year { get; set; }
    }
}
