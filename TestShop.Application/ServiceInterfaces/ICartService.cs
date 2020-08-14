using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TestShop.Application.Models.Cart;
using TestShop.Application.Models.Product;
using TestShop.Domain.Models;

namespace TestShop.Application.ServiceInterfaces
{
    public interface ICartService
    {
        IList<CartProductModel> Get(string userId);
        void Add(string userId, AddToCartModel model);
        void Remove(string userId, int orderId, int productId);
        void Buy(string userId, int id);
        int GetProductsCount(string userId);
    }
}