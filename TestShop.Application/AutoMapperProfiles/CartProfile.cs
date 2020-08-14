using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TestShop.Application.Models.Cart;
using TestShop.Domain.Models;

namespace TestShop.Application.AutoMapperProfiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<OrderProduct, CartProductModel>()
                .ForMember(destMember => destMember.ProductModel,
                    memberOptions =>
                        memberOptions.MapFrom(x => x.Product))
                .ReverseMap();

            CreateMap<AddToCartModel, OrderProduct>().ReverseMap();
        }
    }
}