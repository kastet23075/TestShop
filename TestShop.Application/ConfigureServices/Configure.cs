using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TestShop.Application.AutoMapperProfiles;
using TestShop.Application.Optionns;
using TestShop.Application.ServiceInterfaces;
using TestShop.Application.Services;
using TestShop.Domain;

namespace TestShop.Application.ConfigureServices
{
    public static class Configure
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddEntityFrameworkStoresCustom()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthenticateOptions.ISSUER,
                            ValidateAudience = true,
                            ValidAudience = AuthenticateOptions.AUDIENCE,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthenticateOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<ICartHistoryService, CartHistoryService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new ShopProfile());
                mc.AddProfile(new CartProfile());
                mc.AddProfile(new CartHistoryProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IEmailSender, EmailSender>(i =>
                new EmailSender(
                    configuration["EmailSender:Host"],
                    configuration.GetValue<int>("EmailSender:Port"),
                    configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    configuration.GetValue<string>("TestShopAppUserName"),
                    configuration.GetValue<string>("TestShopAppPassword")
                )
            );

            services.AddSingleton<IConfiguration>(provider => configuration);
        }

        public static IdentityBuilder AddEntityFrameworkStoresCustom(this IdentityBuilder builder)
        {
            builder.AddEntityFrameworkStores<ApplicationDbContext>();
            return builder;
        }
    }
}