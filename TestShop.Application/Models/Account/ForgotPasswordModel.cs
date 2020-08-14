using System.ComponentModel.DataAnnotations;

namespace TestShop.Application.Models.Account
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
