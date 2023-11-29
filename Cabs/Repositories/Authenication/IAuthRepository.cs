
using Cabs.Models.Authenication.Email;
using Cabs.Models.Authenication.Login;
using Cabs.Models.Authenication.Register;
using TaxiDemo.Models;

namespace Cabs.Repositories.Authenication
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
