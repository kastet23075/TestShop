using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TestShop.Application.Models.Product;
using TestShop.Domain.Models;

namespace TestShop.Application.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>()
                .ForSourceMember(destMember => destMember.Id,
                    memberOptions => memberOptions.DoNotValidate());
        }
    }
}