using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using TestShop.Application.ServiceInterfaces;

namespace TestShop.Web.Components
{
    public class CountProductsCart : ViewComponent
    {
        private readonly ICartService cartService;

        public CountProductsCart(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public IViewComponentResult Invoke()
        {
            var userId = Request.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var countProducts = cartService.GetProductsCount(userId);
            var content = countProducts > 0 ? countProducts.ToString() : string.Empty;

            return new HtmlContentViewComponentResult(new HtmlString(content));
        }
    }
}