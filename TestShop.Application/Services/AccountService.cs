using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TestShop.Application.Models.Account;
using TestShop.Application.ServiceInterfaces;
using System.Text.Encodings.Web;
using TestShop.Application.Models.User;
using TestShop.CrossCutting.Enums;
using System.Security.Claims;
using System.Collections.Generic;
using System;
using System.IdentityModel.Tokens.Jwt;
using TestShop.Application.Optionns;
using Microsoft.IdentityModel.Tokens;

namespace TestShop.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IEmailSender emailSender;
        private readonly IUserService userService;

        public AccountService(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, IEmailSender emailSender, 
            IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.userService = userService;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterModel model)
        {
            var identityUser = new IdentityUser {Email = model.Email, UserName = model.Email};
            var result =  await userManager.CreateAsync(identityUser, model.Password);
            if (result.Succeeded)
            {
                await userService.SetRoleAsync(new UserEditModel { Id = identityUser.Id, Role = RolesEnum.User });
                await signInManager.SignInAsync(identityUser, false);
            }

            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginModel model)
        {
            return await signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, false);
        }

        public async Task<IdentityUser> ForgotPasswordAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<string> GetPasswordResetTokenAsync(IdentityUser user)
        {
            return await userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(IdentityUser user, string code, string newPassword)
        {
            return await userManager.ResetPasswordAsync(user, code, newPassword);
        }

        public async Task SendEmailForResetPasswordAsync(string email, string callbackUrl)
        {
            await emailSender.SendEmailAsync(
                email,
                "Reset password",
                $"For reset your password click <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>here</a>.");
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<bool> VerifyEmailAsync(string email)
        {
            var user = await userManager.FindByNameAsync(email);
            return user == null;
        }

        public async Task<bool> VerifyPasswordAsync(string email, string password)
        {
            return await userManager.CheckPasswordAsync(await userManager.FindByEmailAsync(email), password);
        }

        public async Task<string> GetTokenAsync(string email)
        {
            var identity = await GetIdentity(email);

            if (identity == null)
            {
                return string.Empty;
            }

            var timeNow = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthenticateOptions.ISSUER,
                audience: AuthenticateOptions.AUDIENCE,
                notBefore: timeNow,
                claims: identity.Claims,
                expires: timeNow.Add(TimeSpan.FromMinutes(AuthenticateOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthenticateOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private async Task<ClaimsIdentity> GetIdentity(string email)
        {
            var user = await userManager.FindByNameAsync(email);

            if (user != null)
            {                
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, (await userService.GetRoleInUserAsync(user.Id)).ToString())
                };

                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}