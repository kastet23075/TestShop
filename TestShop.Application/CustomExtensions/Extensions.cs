using System;
using System.Collections.Generic;
using System.Text;

namespace TestShop.Application.CustomExtensions
{
    public static class Extensions
    {
        public static string ToStringLower(this bool value)
        {
            return value.ToString().ToLower();
        }
    }
}