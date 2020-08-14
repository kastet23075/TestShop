using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestShop.Application.Models.Cart;
using TestShop.Application.ServiceInterfaces;
using TestShop.Domain;
using TestShop.Domain.Models;

namespace TestShop.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CartService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IList<CartProductModel> Get(string userId)
        {
            var order = context.Orders
                .Include(x => x.OrderProducts)
                .ThenInclude(y => y.Product)
                .FirstOrDefault(x => x.UserId == userId && x.IsProcessed == false);

            return order == null
                ? null
                : mapper.Map<IList<CartProductModel>>(order.OrderProducts);
        }

        public void Add(string userId, AddToCartModel model)
        {
            var order = context.Orders.FirstOrDefault(x => x.UserId == userId && x.IsProcessed == false);

            if (order != null)
            {
                if (CheckProduct(order.Id, model.ProductId))
                {
                    var orderProduct = context.OrderProducts.First(x => x.OrderId == order.Id && x.ProductId == model.ProductId);
                    orderProduct.Count += model.Count;
                    context.OrderProducts.Update(orderProduct);
                }
                else
                {
                    var orderProduct = mapper.Map<OrderProduct>(model);
                    orderProduct.OrderId = order.Id;
                    context.OrderProducts.Add(orderProduct);
                }
            }
            else
            {
                order = Create(userId);
                var orderProduct = mapper.Map<OrderProduct>(model);
                orderProduct.OrderId = order.Id;
                context.OrderProducts.Add(orderProduct);
            }

            context.SaveChanges();
        }

        public void Remove(string userId, int orderId, int productId)
        {
            var orderProduct = context.OrderProducts.FirstOrDefault(x =>
                x.Order.UserId == userId && x.OrderId == orderId && x.ProductId == productId);

            if (orderProduct != null)
            {
                context.OrderProducts.Remove(orderProduct);
                context.SaveChanges();
            }
        }

        public void Buy(string userId, int id)
        {
            var order = context.Orders.FirstOrDefault(x => x.UserId == userId && x.Id == id);

            if (order != null)
            {
                order.IsProcessed = true;
                context.Orders.Update(order);
                context.SaveChanges();
            }
        }

        public int GetProductsCount(string userId)
        {
            var order = context.Orders.FirstOrDefault(x => x.UserId == userId && x.IsProcessed == false);

            return order == null ? 0 : context.OrderProducts.Count(x => x.OrderId == order.Id);
        }

        private Order Create(string userId)
        {
            var order = new Order { UserId = userId, IsProcessed = false };
            context.Orders.Add(order);
            context.SaveChanges();

            return order;
        }

        private bool CheckProduct(int orderId, int productId)
        {
            return context.OrderProducts.Count(x => x.OrderId == orderId && x.ProductId == productId) > 0;
        }
    }
}