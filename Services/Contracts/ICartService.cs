using Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(string userId);
        Task AddItemToCartAsync(string userId, int productId, int quantity);
        Task RemoveItemAsync(int cartItemId);
        Task ClearCartItemsAsync(string userId);
        Task UpdateCartItemQuantityAsync(int cartItemId, int quantity);
    }
}
