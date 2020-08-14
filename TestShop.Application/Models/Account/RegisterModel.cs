using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TestShop.Application.Models.Account
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        [Remote(controller: "Account", action: "VerifyEmail", ErrorMessage = "This email is already in use")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(150, ErrorMessage = "Password length must be at least 6 symbols", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}