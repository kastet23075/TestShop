using System;
using System.Collections.Generic;
using System.Text;
using TestShop.Application.Models.Product;

namespace TestShop.Application.Models.Cart
{
    public class CartProductModel
    {
        public int OrderId { get; set; }
        public ProductModel ProductModel { get; set; }
        public int Count { get; set; }
    }
}