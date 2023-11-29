﻿using System.ComponentModel.DataAnnotations;

namespace Cabs.Models.Authenication.Login
{
    public class LoginModel
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Role { get; set; }

    }
}
