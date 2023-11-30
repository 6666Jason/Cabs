using System.ComponentModel.DataAnnotations;

namespace Cabs.Areas.Website.Models.Authenication.Register
{
    public class RegisterModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Role { get; set; }
    }
}
