using Cabs.Areas.Website.Models;
using Cabs.Areas.Website.Models.Authenication.Email;
using Cabs.Areas.Website.Models.Authenication.Login;
using Cabs.Areas.Website.Models.Authenication.Register;

namespace Cabs.Areas.Website.Repositories.Authenication
{
    public interface IAuthRepository
    {
        Task<IEnumerable<ListAdmin>> ListAdmin();
        Task<IEnumerable<ListDriver>> ListDriver();
        Task<IEnumerable<ListOrganization>> ListOrganization();
        Task<RegisterModel> Register(RegisterModel model);
        Task<string> Login(LoginModel model);
        Task SendMail(MailConfig mailConfig);
        Task<User> ChangePassword(ChangePassword changePassword);
        Task<User> ResetPassword(string email);
    }
}
