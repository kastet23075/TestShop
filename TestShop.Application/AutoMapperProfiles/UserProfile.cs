using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TestShop.Application.Models.User;

namespace TestShop.Application.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<IdentityUser, UserEditModel>()
                .ForMember(destMember => destMember.IsLock,
                    memberOptions => 
                        memberOptions.MapFrom(x => GetIsLock(x.LockoutEnd)));
            CreateMap<UserEditModel, IdentityUser>()
                .ForSourceMember(destMember => destMember.Id,
                    memberOptions => memberOptions.DoNotValidate())
                .ForSourceMember(destMember => destMember.IsLock,
                    memberOptions => memberOptions.DoNotValidate());

            CreateMap<IdentityUser, UserIndexModel>()
                .ForMember(destMember => destMember.IsLock,
                    memberOptions =>
                        memberOptions.MapFrom(x => GetIsLock(x.LockoutEnd)));
        }
        
        private bool GetIsLock(DateTimeOffset? value)
        {
            return value != null ? true : false;
        }
    }
}
