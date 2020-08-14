using System;
using System.Collections.Generic;
using System.Text;

namespace TestShop.Application.Models.CartHistory
{
    public class CartHistoryViewModel
    {
        public int OrderId { get; set; }
        public IList<OrderProductModel> OrderProductModels { get; set; }
        public decimal TotalPrice { get; set; }
    }
}