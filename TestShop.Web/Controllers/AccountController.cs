using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestShop.Application.Models.Account;
using TestShop.Application.ServiceInterfaces;

namespace TestShop.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountService.RegisterAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if(!await accountService.VerifyPasswordAsync(model.Email, model.Password))
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password!");
                    return View(model);
                }

                var result = await accountService.LoginAsync(model);
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "User with this email address is blocked!");
                }
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await accountService.ForgotPasswordAsync(model.Email);

                if (user == null)
                {
                    return View("ForgotPasswordConfirmation");
                }
                else
                {
                    var secretCode = await accountService.GetPasswordResetTokenAsync(user);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { code = secretCode }, protocol: Request.Scheme);
                    await accountService.SendEmailForResetPasswordAsync(model.Email, callbackUrl);

                    return View("ForgotPasswordConfirmation");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await accountService.ForgotPasswordAsync(model.Email);

            if (user == null)
            {
                return View("ResetPasswordConfirmation");
            }

            var result = await accountService.ResetPasswordAsync(user, model.Code, model.Password);

            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                return Json(await accountService.VerifyEmailAsync(model.Email));
            }

            return Json(false);
        }
    }
}