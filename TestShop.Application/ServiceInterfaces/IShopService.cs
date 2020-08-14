using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestShop.Application.Models.Shop;

namespace TestShop.Application.ServiceInterfaces
{
    public interface IShopService
    {
        IList<ProductModel> GetAllProductsAsync();
    }
}