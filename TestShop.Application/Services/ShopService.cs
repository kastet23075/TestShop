using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestShop.Application.Models.Shop;
using TestShop.Application.ServiceInterfaces;
using TestShop.Domain;
using AutoMapper;

namespace TestShop.Application.Services
{
    public class ShopService : IShopService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ShopService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IList<ProductModel> GetAllProductsAsync()
        {
            return mapper.Map<IList<ProductModel>>(context.Products);
        }
    }
}