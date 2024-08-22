using Application.Data.Common;
using Application.Data.Interfaces;

namespace Application.Data
{
    public class MaritalStatus : BaseEntity, IAuditable
    {
        public string Name { get; set; }
    }
}
