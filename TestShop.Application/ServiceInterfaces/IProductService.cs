using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestShop.Application.Models.Product;

namespace TestShop.Application.ServiceInterfaces
{
    public interface IProductService
    {
        Task<IList<ProductModel>> GetAllAsync();
        Task AddAsync(ProductModel model);
        Task DeleteAsync(int id);
        Task EditAsync(ProductModel model);
        Task<ProductModel> GetAsync(int id);
        ProductModel GetModel();
    }
}