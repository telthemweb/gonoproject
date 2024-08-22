using Application.Data.Common;
using Application.Data.Interfaces;

namespace Application.Data
{
    public class Visitor : BaseEntity, IAuditable
    {
        public string? VisitorNumber { get; set; }
        public string? Profile { get; set; }
        public int? TitleId { get; set; }
        public int? GenderId { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? FullName { get; set; }
        public string? PreviousName { get; set; }

        public string? Dob { get; set; }
        public int? Age { get; set; }
        public int? MaritalStatusId { get; set; }
        public string? IDnumber { get; set; }
        public int? NationalityId { get; set; }
        public int? ProvinceId { get; set; }
        public int? CityId { get; set; }

        public MaritalStatus? MaritalStatus { get; set; }
        public Nationality? Nationality { get; set; }
        public City? City { get; set; }
        public Province? Province { get; set; }
        public Gender? Gender { get; set; }
        public Title? Title { get; set; }
    }
}
