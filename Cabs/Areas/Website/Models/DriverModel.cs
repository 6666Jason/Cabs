using Cabs.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cabs.Areas.Website.Models
{
    public class DriverModel : BaseModel
    {
        [ForeignKey("CompanyFkId")]
        public int CompanyFkId { get; set; }  // Foreign Key to link with the Company model
        public string? ContactPerson { get; set; }
        public string? City { get; set; }
        public string? Mobile { get; set; }
        public string? Telephone { get; set; }
        public int Experience { get; set; }
        public string? Description { get; set; }
        public string? PaymentType { get; set; }
        public string? Brand { get; set; }   // Thương hiệu
        public string? Model { get; set; }   // Mô hình
        public int Year { get; set; }       // Năm sản xuất
        public string? Color { get; set; }   // Màu sắc
        public string? LicensePlate { get; set; } // Biển số xe
        public CompanyModel? Company { get; set; }
        [ForeignKey("UserFkId")]
        public int UserFkId { get; set; }
        public User? User { get; set; }

        public virtual ICollection<BookingDriver>? BookingDrivers { get; set; } = new HashSet<BookingDriver>();

        public virtual ICollection<AdvertiseModel>? Advertises { get; set; }

        public virtual ICollection<ImageModel>? Images { get; set; }


    }
}
