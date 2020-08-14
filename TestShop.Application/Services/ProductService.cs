using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestShop.Application.Models.Product;
using TestShop.Application.ServiceInterfaces;
using TestShop.Domain;
using TestShop.Domain.Models;
using AutoMapper;

namespace TestShop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IList<ProductModel>> GetAllAsync()
        {
            return mapper.Map<IList<ProductModel>>(context.Products);
        }

        public async Task AddAsync(ProductModel model)
        {
            context.Products.Add(mapper.Map<Product>(model));
            context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }

        public async Task EditAsync(ProductModel model)
        {
            context.Products.Update(mapper.Map<Product>(model));
            context.SaveChanges();
        }

        public async Task<ProductModel> GetAsync(int id)
        {
            return mapper.Map<ProductModel>(context.Products.Find(id));
        }

        public ProductModel GetModel()
        {
            return new ProductModel();
        }
    }
}