using Cabs.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cabs.Areas.Website.Models
{
    public class CompanyModel : BaseModel
    {
        public string? ContactPerson { get; set; }
        public string? Designation { get; set; }
        public string? Mobile { get; set; }
        public string? Telephone { get; set; }
        public string? FaxNumber { get; set; }
        public string? MembershipType { get; set; }
        public string? PaymentType { get; set; }
        [ForeignKey("UserFkId")]
        public int UserFkId { get; set; }
        public User? User { get; set; }
        public virtual ICollection<DriverModel>? Drivers { get; set; }
        public virtual ICollection<AdvertiseModel>? Advertises { get; set; }

        public virtual ICollection<BookingCompany>? BookingCompanies { get; set; } = new HashSet<BookingCompany>();
        public virtual ICollection<ImageModel>? Images { get; set; }

    }
}
