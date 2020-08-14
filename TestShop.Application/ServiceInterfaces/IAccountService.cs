using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TestShop.Application.Models.Account;

namespace TestShop.Application.ServiceInterfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterModel model);
        Task<SignInResult> LoginAsync(LoginModel model);
        Task<IdentityUser> ForgotPasswordAsync(string email);
        Task<string> GetPasswordResetTokenAsync(IdentityUser user);
        Task<IdentityResult> ResetPasswordAsync(IdentityUser user, string code, string newPassword);
        Task SendEmailForResetPasswordAsync(string email, string callbackUrl);
        Task LogoutAsync();
        Task<bool> VerifyEmailAsync(string email);
        Task<bool> VerifyPasswordAsync(string email, string password);
        Task<string> GetTokenAsync(string email);
    }
}