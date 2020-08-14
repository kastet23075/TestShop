using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TestShop.Domain.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool IsProcessed { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
    }
}