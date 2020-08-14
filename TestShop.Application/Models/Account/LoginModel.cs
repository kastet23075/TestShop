using System.ComponentModel.DataAnnotations;

namespace TestShop.Application.Models.Account
{
    public class LoginModel
    {
        [EmailAddress]
        [Required (ErrorMessage = "Enter your email address!")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Enter your password!")]
        [StringLength(150, ErrorMessage = "Password length must be at least 6 symbols", MinimumLength = 6)]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}
