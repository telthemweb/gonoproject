using Application.Data.Common;
using Application.Data.Interfaces;

namespace Application.Data
{
    public class Province : BaseEntity, IAuditable
    {
        public string Name { get; set; }
    }
}
