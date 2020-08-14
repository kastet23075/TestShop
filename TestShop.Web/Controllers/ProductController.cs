using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestShop.Application.Models.Product;
using TestShop.Application.ServiceInterfaces;
using TestShop.CrossCutting.Enums;

namespace TestShop.Web.Controllers
{
    [Authorize(Roles = nameof(RolesEnum.Administrator))]
    public class ProductController : BaseController
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await productService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != 0)
            {
                return View(nameof(Edit), await productService.GetAsync(id));
            }
            
            return View(nameof(Index));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(nameof(Edit), productService.GetModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await productService.AddAsync(model);
                    return RedirectToAction(nameof(Index));
                }

                await productService.EditAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(nameof(Edit), model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}