﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Net.Mime;
using Cabs.Data;
using Cabs.Areas.Website.Models;
using Cabs.Areas.Website.Models.Authenication;
using Cabs.Areas.Website.Models.Authenication.Email;
using Cabs.Areas.Website.Models.Authenication.Login;
using Cabs.Areas.Website.Models.Authenication.Register;
using Cabs.Areas.Website.Repositories.Authenication;

namespace Cabs.Areas.Website.Services.Authenication
{
    public class AuthServiceImp : IAuthRepository
    {
        private readonly UserManager<User> userManager;
        private readonly DatabaseContext dbcontext;
        private readonly IConfiguration configuration;

        public AuthServiceImp(UserManager<User> _userManager, DatabaseContext _dbcontext, IConfiguration _configuration)
        {
            userManager = _userManager;
            dbcontext = _dbcontext;
            configuration = _configuration;
        }
        public async Task<RegisterModel> Register(RegisterModel model)
        {
            // check email exist
            try
            {
                var emailExist = await userManager.FindByEmailAsync(model.Email);
                if (emailExist == null)
                {
                    var user = new User
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Role = model.Role,
                        CustomEmail = model.Email,
                        Password = SecurityAccount.EncodePlanText(model.Password),
                        UserName = model.Email

                    };

                    var result = await userManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        return model;
                    }
                    else { return null; }
                }
                else { return null; }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public async Task<string> Login(LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null && await CheckPassword(user.Password, model.Password))
            {
                // generic token
                var token = GenerateToken(user);
                return token;
            }
            else { return null; }
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials
                (securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Name",user.Name),
                new Claim("Email",user.Email),
                new Claim(ClaimTypes.Role,user.Role)
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<bool> CheckPassword(string passwordDb, string? password)
        {
            return SecurityAccount.DecodePlanText(passwordDb).Equals(password);
        }

        public async Task<IEnumerable<ListAdmin>> ListAdmin()
        {
            var list = await dbcontext.Users.Where(u => u.Role == RoleModel.Admin)
                                            .Select(l => new ListAdmin
                                            {
                                                Id = l.Id,
                                                Name = l.Name,
                                                Email = l.Email,
                                            }).ToListAsync();
            if (list.Count > 0 && list.Any())
            {
                return list;
            }
            return null;
        }

        public async Task<IEnumerable<ListDriver>> ListDriver()
        {
            var list = await dbcontext.Users.Where(u => u.Role == RoleModel.Driver)
                                            .Select(l => new ListDriver
                                            {
                                                Id = l.Id,
                                                Name = l.Name,
                                                Email = l.Email,
                                            }).ToListAsync();
            if (list.Count > 0 && list.Any())
            {
                return list;
            }
            return null;
        }

        public async Task<IEnumerable<ListOrganization>> ListOrganization()
        {
            var list = await dbcontext.Users.Where(u => u.Role == RoleModel.Organization)
                                            .Select(l => new ListOrganization
                                            {
                                                Id = l.Id,
                                                Name = l.Name,
                                                Email = l.Email,
                                            }).ToListAsync();
            if (list.Count > 0 && list.Any())
            {
                return list;
            }
            return null;
        }

        public async Task SendMail(MailConfig mailConfig)
        {
            var message = new MailMessage(FromMail.MailSend, mailConfig.ToMail);
            message.Subject = mailConfig.Subject;
            message.Body = mailConfig.Body;
            message.IsBodyHtml = true;

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailConfig.Body, null, MediaTypeNames.Text.Html);
            message.AlternateViews.Add(htmlView);

            using (var clientMail = new SmtpClient())
            {
                clientMail.Host = "smtp.gmail.com";
                clientMail.Port = 587;
                // Đặt thành false để chỉ ra rằng chúng ta muốn sử dụng thông tin đăng nhập cụ thể (tên đăng nhập và mật khẩu)
                clientMail.UseDefaultCredentials = false;
                clientMail.EnableSsl = true;
                clientMail.Credentials = new NetworkCredential(FromMail.MailSend, FromMail.PasswordMail);
                clientMail.Send(message);
            }

        }

        public async Task<User> ChangePassword(ChangePassword changePassword)
        {
            var userExist = await userManager.FindByEmailAsync(changePassword.Email);
            if (userExist != null && await CheckPassword(userExist.Password, changePassword.OldPassword))
            {
                userExist.Password = SecurityAccount.EncodePlanText(changePassword.NewPassword);
                await userManager.UpdateAsync(userExist);
                return userExist;
            }
            else { return null; }
        }

        public async Task<User> ResetPassword(string email)
        {
            var userExist = await userManager.FindByEmailAsync(email);
            if (userExist != null)
            {
                userExist.Password = SecurityAccount.EncodePlanText("fptAptech");
                await userManager.UpdateAsync(userExist);
                return userExist;
            }
            else { return null; }
        }
    }
}
