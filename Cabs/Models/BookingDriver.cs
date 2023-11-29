using Cabs.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiDemo.Models
{
    public class BookingDriver : BaseModel
    {
        [ForeignKey("BookingFkId")]
        public int BookingFkId { get; set; }
        public BookingModel? Booking { get; set; }

        [ForeignKey("DriverFkId")]
        public int DriverFkId { get; set; }
        public DriverModel? Driver { get; set; }
    }
}
