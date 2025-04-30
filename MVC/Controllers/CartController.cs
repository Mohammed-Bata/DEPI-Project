using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Services;
using Services.Contracts;
using Stripe.Climate;
using System.Security.Claims;

namespace MVC.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly TokenProviderService _tokenProviderService;
        
        public CartController(CartService cartService, TokenProviderService tokenProviderService)
        {
            _cartService = cartService;
            _tokenProviderService = tokenProviderService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartService.GetCartAsync(userId);
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = await _cartService.AddItemToCartAsync(userId, productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(int cartItemId, int newQuantity)
        {
            var success = await _cartService.UpdateQuantityAsync(cartItemId, newQuantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            var success = await _cartService.RemoveItemAsync(cartItemId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = await _cartService.ClearCartAsync(userId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ProceedToPay()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var cart = await _cartService.GetCartAsync(userId);
            if (cart == null || cart.Items.Count == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            ViewBag.CartItemCount = cart.Items.Count;
            ViewBag.CartTotal = cart.TotalPrice;

            return View();
        }
    }
}
