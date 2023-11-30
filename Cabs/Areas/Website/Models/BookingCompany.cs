using Cabs.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Cabs.Areas.Website.Models
{
    public class BookingCompany : BaseModel
    {
        [ForeignKey("BookingFkId")]
        public int BookingFkId { get; set; }
        public BookingModel? Booking { get; set; }


        [ForeignKey("CompanyFkId")]
        public int CompanyFkId { get; set; }
        public CompanyModel? Company { get; set; }
    }
}
