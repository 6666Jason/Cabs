using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cabs.Areas.Website.Models;
using Cabs.Areas.Website.Models.Authenication;
using Cabs.Areas.Website.Models.CustomResponse;
using Cabs.Areas.Website.Models.Authenication.Email;
using Cabs.Areas.Website.Models.Authenication.Login;
using Cabs.Areas.Website.Models.Authenication.Register;
using Cabs.Areas.Website.Repositories.Authenication;

namespace Cabs.Areas.Website.Controllers
{
    [Route("api/[controller]/[action]")]
    [Area("Website")]
    [ApiController]
    public class AuthenicationController : ControllerBase
    {
        private readonly IAuthRepository authRepo;
        private readonly UserManager<User> userManager;

        public AuthenicationController(IAuthRepository _authRepo, UserManager<User> _userManager)
        {
            authRepo = _authRepo;
            userManager = _userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            try
            {
                var result = await authRepo.Register(registerModel);
                if (result != null)
                {
                    return CustomMethodResponse.GetResponse201Created(result, "Create user success");
                }
                else
                {
                    return CustomMethodResponse.GetResponse400BadResquest("Create user fail");
                }
            }
            catch (Exception ex)
            {
                return CustomMethodResponse.Response500Error(ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try
            {
                var token = await authRepo.Login(loginModel);
                if (token != null)
                {
                    var user = await userManager.FindByEmailAsync(loginModel.Email);
                    if (user != null)
                    {
                        var inforUser = new InfoUserLogin
                        {
                            Id = user.Id,
                            Image = user.Image,
                            Name = user.Name,
                            Email = user.Email,
                            Role = user.Role
                        };
                        if (loginModel.Role != null)
                        {
                            if (loginModel.Role.Equals(RoleModel.Admin))
                            {
                                if (user.Role.Equals(RoleModel.Organization) || user.Role.Equals(RoleModel.Driver))
                                {
                                    return CustomMethodResponse.GetResponse400BadResquest("This account has not been registered, please contact the manager to get an account");
                                }
                            }
                        }
                        return CustomMethodResponse.GetResponse200Ok(new { token, inforUser }, "Login success");
                    }
                }
                return CustomMethodResponse.GetResponse400BadResquest("Login fail");
            }
            catch (Exception ex)
            {
                return CustomMethodResponse.Response500Error(ex);
            }

        }

        [HttpGet]
        public async Task<IActionResult> ListAdmin()
        {
            try
            {
                var result = await authRepo.ListAdmin();
                if (result != null)
                {
                    return CustomMethodResponse.GetListResponse200Ok(result, "Get list success");
                }
                else
                {
                    return CustomMethodResponse.GetResponse404NotFound("Get list fail");
                }
            }
            catch (Exception ex)
            {
                return CustomMethodResponse.Response500Error(ex);
            }

        }

        [HttpGet]
        public async Task<IActionResult> ListDriver()
        {
            try
            {
                var result = await authRepo.ListDriver();
                if (result != null)
                {
                    return CustomMethodResponse.GetListResponse200Ok(result, "Get list success");
                }
                else
                {
                    return CustomMethodResponse.GetResponse404NotFound("Get list fail");
                }
            }
            catch (Exception ex)
            {
                return CustomMethodResponse.Response500Error(ex);
            }

        }

        [HttpGet]
        public async Task<IActionResult> ListOrganization()
        {
            try
            {
                var result = await authRepo.ListOrganization();
                if (result != null)
                {
                    return CustomMethodResponse.GetListResponse200Ok(result, "Get list success");
                }
                else
                {
                    return CustomMethodResponse.GetResponse404NotFound("Get list fail");
                }
            }
            catch (Exception ex)
            {
                return CustomMethodResponse.Response500Error(ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(MailConfig mailConfig)
        {
            try
            {
                var result = authRepo.SendMail(mailConfig);

                if (result.IsCompleted)
                {
                    return CustomMethodResponse.GetResponse200Ok(result, "Send email success");
                }
                else
                {
                    return CustomMethodResponse.GetResponse400BadResquest("Send mail fail");
                }
            }
            catch (Exception ex)
            {
                return CustomMethodResponse.Response500Error(ex);
            }

        }

        [HttpPut("{email}")]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
        {
            try
            {
                var result = await authRepo.ChangePassword(changePassword);

                if (result != null)
                {
                    return CustomMethodResponse.GetResponse200Ok(result, "Change Password success");
                }
                else
                {
                    return CustomMethodResponse.GetResponse400BadResquest("Change Password fail");

                }
            }
            catch (Exception ex)
            {
                return CustomMethodResponse.Response500Error(ex);
            }
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> ResetPassword(string email)
        {
            try
            {
                var result = await authRepo.ResetPassword(email);
                if (result != null)
                {
                    return CustomMethodResponse.GetResponse200Ok(result, "Reset Password success");
                }
                else
                {
                    return CustomMethodResponse.GetResponse400BadResquest("Reset Password fail");
                }
            }
            catch (Exception ex)
            {
                return CustomMethodResponse.Response500Error(ex);
            }
        }

    }
}
