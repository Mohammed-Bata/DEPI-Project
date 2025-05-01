using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.Dtos;
using MVC.Services;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync<Product>();
            return View(products);
        }

        public async Task<IActionResult> ProductReviews(int id)
        {
            var product = await _productService.GetProductWithReviewsAsync(id);
            return View(product);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto model)
        {
            //if (!ModelState.IsValid)
            //    return View(model);
            //var result = await _productService.CreateProductAsync(model);

            //if (result)
            //    return RedirectToAction("Index");

            //ModelState.AddModelError("", "Error while creating product");
            // return View(model);

            var model2 = new ProductDto { Name = "Test", Price = 100, Description = "desc" };
            
            var result = await _productService.CreateProductAsync(model2);

            if (result.IsSuccess)
            {
                //return Ok(new { message = "Product created successfully." });
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
    }
}
