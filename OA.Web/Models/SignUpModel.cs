﻿using Microsoft.AspNetCore.Mvc;
using OA.Core;
using System.ComponentModel.DataAnnotations;

namespace OA_WEB.Models
{
    public class SignUpModel 
    {
        [Required(ErrorMessage = "Please enter username")]
        [Remote(action: "UserNameIsExist", controller: "Account")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter Valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter mobile number")]
        [Display(Name = "Mobile Number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("Password", ErrorMessage = ("confirm password can't matched!"))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string Address { get; set; }
    }
}
