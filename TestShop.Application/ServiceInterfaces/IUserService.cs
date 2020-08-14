using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestShop.Application.Models.User;
using TestShop.CrossCutting.Enums;

namespace TestShop.Application.ServiceInterfaces
{
    public interface IUserService
    {
        Task<IList<UserIndexModel>> GetAllAsync();
        Task<UserEditModel> GetAsync(string id);
        Task SetRoleAsync(UserEditModel model);
        Task ChangeLockStatusAsync(UserLockModel model);
        Task LogOutAsync(IdentityUser user);
        Task<RolesEnum> GetRoleInUserAsync(string id);
    }
}
