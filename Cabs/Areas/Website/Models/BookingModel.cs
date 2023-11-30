using Cabs.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cabs.Areas.Website.Models
{
    public class BookingModel : BaseModel
    {
        [ForeignKey("CustomerFkId")]
        public int CustomerFkId { get; set; }  // Foreign Key to link with the Customer model
        [ForeignKey("CompanyFkId")]
        public int CompanyFkId { get; set; }   // Foreign Key to link with the Company model
        [ForeignKey("DriverFkId")]
        public int DriverFkId { get; set; }    // Foreign Key to link with the Driver model
        public DateTime PickupDateTime { get; set; }
        public DateTime DropDateTime { get; set; }
        public string? PickupLocation { get; set; }
        public string? DropLocation { get; set; }
        public BookingStatus Status { get; set; }
        public User? User { get; set; }
        public PaymentModel? Payment { get; set; }

        public virtual ICollection<BookingCompany>? BookingCompanies { get; set; } = new HashSet<BookingCompany>();
        public virtual ICollection<BookingDriver>? BookingDrivers { get; set; } = new HashSet<BookingDriver>();
    }

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Completed,
        Canceled
    }
}
