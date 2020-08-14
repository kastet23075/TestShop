using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestShop.Application.Models.CartHistory;
using TestShop.Application.ServiceInterfaces;
using TestShop.Domain;

namespace TestShop.Application.Services
{
    public class CartHistoryService : ICartHistoryService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CartHistoryService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IList<CartHistoryViewModel> Get(string userId)
        {
            var orders = context.Orders
                .Include(x => x.OrderProducts)
                .ThenInclude(y => y.Product)
                .Where(x => x.UserId == userId && x.IsProcessed)
                .OrderByDescending(x => x.Id);

            return mapper.Map<IList<CartHistoryViewModel>>(orders);
        }
    }
}