using System;
using System.Collections.Generic;
using System.Text;

namespace TestShop.Application.Models.Shop
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}