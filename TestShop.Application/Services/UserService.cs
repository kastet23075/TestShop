using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TestShop.Application.Models.User;
using TestShop.Application.ServiceInterfaces;
using TestShop.CrossCutting.Enums;

namespace TestShop.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public async Task<IList<UserIndexModel>> GetAllAsync()
        {
            var userIndexModels = mapper.Map<IList<UserIndexModel>>(userManager.Users);
            foreach (var item in userIndexModels)
            {
                item.Role = await GetRoleInUserAsync(item.Id);
            }

            return userIndexModels;
        }

        public async Task<UserEditModel> GetAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userModel = mapper.Map<UserEditModel>(user);
            userModel.Role = await GetRoleInUserAsync(id);
            userModel.Roles = Enum.GetValues(typeof(RolesEnum)).Cast<RolesEnum>();

            return userModel;
        }

        public async Task<RolesEnum> GetRoleInUserAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var identityUserRoles = await userManager.GetRolesAsync(user);
            var userRole = identityUserRoles.First();

            return Enum.Parse<RolesEnum>(userRole);
        }

        public async Task SetRoleAsync(UserEditModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (!await userManager.IsInRoleAsync(user, model.Role.ToString()))
            {
                await userManager.RemoveFromRolesAsync(user, await userManager.GetRolesAsync(user));
                await userManager.AddToRoleAsync(user, model.Role.ToString());
                await LogOutAsync(user);
            }
        }

        public async Task ChangeLockStatusAsync(UserLockModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (model.IsLock)
            {
                user.LockoutEnd = null;
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(100);
                await LogOutAsync(user);
            }

            await userManager.UpdateAsync(user);
        }

        public async Task LogOutAsync(IdentityUser user)
        {
            await userManager.UpdateSecurityStampAsync(user);            
        }
    }
}
