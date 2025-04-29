using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
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
            var products = await _productService.GetProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> Reviews(int id)
        {
            var product = await _productService.GetProductWithReviewsAsync(id);
            return View(product);
        }
    }
}
