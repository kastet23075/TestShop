using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TestShop.Application.Models.Shop;
using TestShop.Domain.Models;

namespace TestShop.Application.AutoMapperProfiles
{
    public class ShopProfile : Profile
    {
        public ShopProfile()
        {
            CreateMap<Product, ProductModel>();
        }
    }
}