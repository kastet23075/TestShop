using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using TestShop.Application.Models.CartHistory;
using TestShop.Domain.Models;

namespace TestShop.Application.AutoMapperProfiles
{
    public class CartHistoryProfile : Profile
    {
        public CartHistoryProfile()
        {
            CreateMap<Order, CartHistoryViewModel>()
                .ForMember(destMember => destMember.OrderId,
                    memberOptions =>
                        memberOptions.MapFrom(x => x.Id))
                .ForMember(destMember => destMember.OrderProductModels,
                    memberOptions =>
                        memberOptions.MapFrom(x => x.OrderProducts))
                .ForMember(destMember => destMember.TotalPrice,
                    memberOptions =>
                        memberOptions.MapFrom(x => GetTotalPrice(x.OrderProducts)));

            CreateMap<OrderProduct, OrderProductModel>()
                .ForMember(destMember => destMember.Name,
                    memberOptions =>
                        memberOptions.MapFrom(x => x.Product.Name))
                .ForMember(destMember => destMember.Price,
                    memberOptions =>
                        memberOptions.MapFrom(x => x.Product.Price));
        }

        private decimal GetTotalPrice(ICollection<OrderProduct> orderProducts)
        {
            decimal totalPrice = 0;
            foreach (var orderProduct in orderProducts)
            {
                totalPrice += orderProduct.Count * orderProduct.Product.Price;
            }

            return totalPrice;
        }
    }
}