using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestShop.CrossCutting.Enums;
using TestShop.Domain.Configuration;
using TestShop.Domain.Models;

namespace TestShop.Domain
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
                { Name = RolesEnum.Administrator.ToString(), NormalizedName = RolesEnum.Administrator.ToString().ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
                { Name = RolesEnum.User.ToString(), NormalizedName = RolesEnum.User.ToString().ToUpper() });
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
        }
    }
}
