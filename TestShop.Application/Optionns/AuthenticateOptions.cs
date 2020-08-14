using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestShop.Application.Optionns
{
    public class AuthenticateOptions
    {
        public const string ISSUER = "AuthenticateServer";
        public const string AUDIENCE = "AuthenticateClient";
        const string KEY = "supersecret_secret_key!8";
        public const int LIFETIME = 5;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
