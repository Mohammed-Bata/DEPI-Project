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
    [Authorize(Roles = "Customer")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Get the user's cart
        [HttpGet]
        public async Task<IActionResult> GetCart(string userId)
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
        public async Task<IActionResult> AddItemToCart(string userId, [FromBody] CartDto data)
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
        public async Task<IActionResult> RemoveItemFromCart(int cartItemId)
        {
            try
            {
                await _cartService.RemoveItemAsync(cartItemId);
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
        public async Task<IActionResult> UpdateQuantity([FromBody] CartDto data)
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
        public async Task<IActionResult> ClearCart(string userId)
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
