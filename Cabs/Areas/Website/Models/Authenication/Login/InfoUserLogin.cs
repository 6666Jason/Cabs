using System.ComponentModel.DataAnnotations;

namespace Cabs.Areas.Website.Models.Authenication.Login
{
    public class InfoUserLogin
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Image { get; set; }
    }
}
