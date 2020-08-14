using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace TestShop.Application.Models.Cart
{
    public class AddToCartModel
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}