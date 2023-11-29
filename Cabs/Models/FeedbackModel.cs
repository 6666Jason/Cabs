using Cabs.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiDemo.Models
{
    public class FeedbackModel : BaseModel
    {
        [ForeignKey("CustomerFkId")]
        public int CustomerFkId { get; set; }  // Foreign Key to link with the Customer model
        public string? Name { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public FeedbackType Type { get; set; }
        public string? Description { get; set; }

        public User? User { get; set; }
    }

    public enum FeedbackType
    {
        Complaint,
        Suggestion,
        Compliment
    }
}
