﻿namespace Cabs.Areas.Website.Models.Authenication.Login
{
    public class ChangePassword
    {
        public string? Email { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
