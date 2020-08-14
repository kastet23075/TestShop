using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestShop.Application.Models.User;
using TestShop.Application.ServiceInterfaces;
using TestShop.CrossCutting.Enums;

namespace TestShop.Web.Controllers
{
    [Authorize(Roles = nameof(RolesEnum.Administrator))]
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await userService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return View(await userService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditModel model)
        {
            await userService.SetRoleAsync(model);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Lock(UserLockModel model)
        {
            await userService.ChangeLockStatusAsync(model);
            return Ok();
        }
    }
}