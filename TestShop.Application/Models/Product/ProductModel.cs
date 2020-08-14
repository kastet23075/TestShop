using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TestShop.Application.Models.Product
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "The field cannot be empty!")] 
        public string Name { get; set; }

        [Required(ErrorMessage = "You did not indicate the price of product!")]
        [Range(typeof(decimal), "0.01", "99999999.99", ErrorMessage = "Value price should be from 0.01 to 99999999.99!")]
        public decimal Price { get; set; }
    }
}