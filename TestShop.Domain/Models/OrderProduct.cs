using System;
using System.Collections.Generic;
using System.Text;

namespace TestShop.Domain.Models
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}