using API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Contracts;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Get the user's cart
        [HttpGet]
        public async Task<IActionResult> GetCartAsync(string userId)
        {
            var cart = await _cartService.GetCartAsync(userId);
            if (cart == null)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Cart not found." }
                });
            }
            return Ok(new APIResponse
            {
                StatusCode = HttpStatusCode.OK,
                Data = cart
            });
        }

        // Add an item to the cart (productId, quantity)
        [HttpPost]
        public async Task<IActionResult> AddItemToCartAsync(string userId, CartDto data)
        {
            if (data.Quantity <= 0)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Quantity must be greater than 0." }
                });
            }
            try
            {
                await _cartService.AddItemToCartAsync(userId, data.Id, data.Quantity);
                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // Remove an item from the cart
        [HttpDelete]
        public async Task<IActionResult> RemoveItemFromCartAsync(RemoveCartItemDto data)
        {
            try
            {
                await _cartService.RemoveItemAsync(data.ItemId);
                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { ex.Message }
                });
            }
            
        }

        // Update item quantity (cartItemId, quantity)
        [HttpPut]
        public async Task<IActionResult> UpdateQuantity(CartDto data)
        {
            if (data.Quantity <= 0)
            {
                return BadRequest(
                new APIResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Quantity must be greater than 0." }
                });
            }
            try
            {
                await _cartService.UpdateCartItemQuantityAsync(data.Id, data.Quantity);
                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // Clear all items in the user's cart
        [HttpDelete]
        public async Task<IActionResult> ClearCartAsync(string userId)
        {
            try
            {
                await _cartService.ClearCartItemsAsync(userId);

                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
}
