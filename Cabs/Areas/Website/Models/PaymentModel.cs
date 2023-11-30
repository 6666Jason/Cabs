using Cabs.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cabs.Areas.Website.Models
{
    public class PaymentModel : BaseModel
    {
        [ForeignKey("CustomerFkId")]
        public int CustomerFkId { get; set; }  // Foreign Key to link with the Customer model
        [ForeignKey("CompanyFkId")]
        public int CompanyFkId { get; set; }
        [ForeignKey("DriverFkId")]
        public int DriverFkId { get; set; }
        // Foreign Key to link with the Booking model
        public decimal Amount { get; set; }
        public DateTime PaymentDatetime { get; set; }
        public PaymentStatus Status { get; set; }
        //public BookingModel? Booking { get; set; }
        [ForeignKey("BookingFkId")]
        public int BookingFkId { get; set; }
        public BookingModel? Booking { get; set; }


    }

    public enum PaymentStatus
    {
        Success,
        Failure,
        Pending
    }
}
