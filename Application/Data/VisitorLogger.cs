using Application.Data.Common;
using Application.Data.Interfaces;
using System;

namespace Application.Data
{
    public class VisitorLogger : BaseEntity, IAuditable
    {
        public int? VisitorId { get; set; }
        public string? Purposeofvisit { get; set;}
        public DateTime? DateCheckin { get; set; }
        public string? TimeCheckin { get; set; }
        public string? TimeCheckout { get; set; }
        public string? Status { get; set; }
    }
}
