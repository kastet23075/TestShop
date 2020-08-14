﻿using System;
using System.Collections.Generic;
using System.Text;
using TestShop.CrossCutting.Enums;

namespace TestShop.Application.Models.User
{
    public class UserIndexModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public RolesEnum Role { get; set; }
        public bool IsLock { get; set; }
    }
}
