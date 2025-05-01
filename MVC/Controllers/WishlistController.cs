using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly WishlistService _wishlistService;
        private readonly TokenProviderService _tokenProviderService;

        public WishlistController(WishlistService wishlistService, TokenProviderService tokenProviderService)
        {
            _wishlistService = wishlistService;
            _tokenProviderService = tokenProviderService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _wishlistService.GetWishlist<APIResponse>();
            Wishlist wishlist = new Wishlist();
            if (response != null)
            {
                wishlist = JsonConvert.DeserializeObject<Wishlist>(Convert.ToString(response.Data));
            }
            return View(wishlist);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int productId)
        {
            var response = await _wishlistService.AddToWishlist<APIResponse>(productId);
            if (response != null)
            {
                TempData["success"] = "Product added to wishlist successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("CustomError", response.Errors.FirstOrDefault());
                TempData["error"] = "Error encountered.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWishlist(int productId)
        {
            var response = await _wishlistService.RemoveFromWishlist<APIResponse>(productId);
            if (response != null)
            {
                TempData["success"] = "Product removed from wishlist successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("CustomError", response.Errors.FirstOrDefault());
                TempData["error"] = "Error encountered.";
                return RedirectToAction(nameof(Index));
            }
        }


    }
}
