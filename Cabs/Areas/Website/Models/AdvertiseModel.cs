using Cabs.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Cabs.Areas.Website.Models
{
    public class AdvertiseModel : BaseModel
    {
        public string? CompanyName { get; set; }
        [ForeignKey("CompanyFkId")]
        public int CompanyFkId { get; set; }  // Foreign Key to link with the Company model
        [ForeignKey("DriverFkId")]
        public int DriverFkId { get; set; }
        public string? Designation { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public string? Telephone { get; set; }
        public string? FaxNumber { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public string? PaymentType { get; set; }

        public CompanyModel? Company { get; set; }
        public DriverModel? Driver { get; set; }
        public virtual ICollection<AdvertiseImageModels>? AdvertiseImages { get; set; }
    }
}
