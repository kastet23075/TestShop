using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestShop.Application.Models.Cart;
using TestShop.Application.ServiceInterfaces;

namespace TestShop.Web.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public IActionResult Index()
        {
            return View(cartService.Get(UserId));
        }

        public IActionResult AddToCart(AddToCartModel model)
        {
            cartService.Add(UserId, model);

            return RedirectToAction(nameof(Index), "Shop");
        }

        public IActionResult RemoveFromCart(int orderId, int productId)
        {
            cartService.Remove(UserId, orderId, productId);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Buy(int orderId)
        {
            cartService.Buy(UserId, orderId);

            return View(nameof(Buy));
        }

        public IActionResult GetProductsCount()
        {
            return Json(cartService.GetProductsCount(UserId));
        }
    }
}