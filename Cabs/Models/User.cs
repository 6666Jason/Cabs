using Cabs.Models.Authenication;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiDemo.Models
{
    public class User : IdentityUser<int>
    {
        public string? Name { get; set; }
        public string? CustomEmail { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; }
        public string? Image { get; set; }
        public CompanyModel? Company { get; set; }
        
        public virtual ICollection<DriverModel>? Drivers { get; set; }
        // Khóa ngoại để liên kết với OtherEntities
        public int OtherEntitiesFkId { get; set; }
        public virtual ICollection<FeedbackModel>? Feedbacks { get; set; }


        public List<BookingModel>? Bookings { get; set; }
    }
}
