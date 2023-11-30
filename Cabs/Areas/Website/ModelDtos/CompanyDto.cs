using Cabs.BaseEntity;
using System.ComponentModel.DataAnnotations;

namespace Cabs.Areas.Website.ModelDtos
{
    public class CompanyDto
    {
        [Key]
        public int? Id { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Designation { get; set; }
        public string? Mobile { get; set; }
        public string? Telephone { get; set; }
        public string? FaxNumber { get; set; }
        public string? MembershipType { get; set; }
        public string? PaymentType { get; set; }
        public string? Image { get; set; }
    }
}
