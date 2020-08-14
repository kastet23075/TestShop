using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestShop.Application.ServiceInterfaces;

namespace TestShop.Web.Controllers
{
    [Authorize]
    public class CartHistoryController : BaseController
    {
        private readonly ICartHistoryService historyService;

        public CartHistoryController(ICartHistoryService historyService)
        {
            this.historyService = historyService;
        }

        public IActionResult Index()
        {
            return View(historyService.Get(UserId));
        }
    }
}