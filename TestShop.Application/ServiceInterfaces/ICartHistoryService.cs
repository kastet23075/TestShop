using System;
using System.Collections.Generic;
using System.Text;
using TestShop.Application.Models.CartHistory;

namespace TestShop.Application.ServiceInterfaces
{
    public interface ICartHistoryService
    {
        IList<CartHistoryViewModel> Get(string userId);
    }
}
