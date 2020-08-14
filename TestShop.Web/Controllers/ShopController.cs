using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestShop.Application.ServiceInterfaces;

namespace TestShop.Web.Controllers
{
    [Authorize]
    public class ShopController : BaseController
    {
        private readonly IShopService shopService;

        public ShopController(IShopService shopService)
        {
            this.shopService = shopService;
        }

        public IActionResult Index()
        {
            return View(shopService.GetAllProductsAsync());
        }
    }
}