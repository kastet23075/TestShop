using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestShop.Application.Models.Account;
using TestShop.Application.ServiceInterfaces;

namespace TestShop.Web.React.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await accountService.LoginAsync(model);

            if (result.IsLockedOut)
            {
                return StatusCode(423);
            }

            return result.Succeeded ? (IActionResult) Ok( await GetToken(model.Email)) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await accountService.LogoutAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (await accountService.VerifyEmailAsync(model.Email))
            {
                var result = await accountService.RegisterAsync(model);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }

            return StatusCode(422);
        }

        [HttpPost]
        public async Task<IActionResult> GetToken(string email)
        {
            var token = await accountService.GetTokenAsync(email);

            if (token == string.Empty)
            {
                return NotFound();
            }

            var response = new
            {
                access_token = token,
                email
            };

            return Ok(response);
        }
    }
}