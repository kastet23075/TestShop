using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestShop.Domain.Models;

namespace TestShop.Domain.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasOne(x => x.IdentityUser)
                .WithMany()
                .HasForeignKey(x => x.UserId);
        }
    }
}