using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task AddItemAsync(string userId, int productId, int quantity);
        Task RemoveItemAsync(int cartItemId);
        Task ClearCartAsync(string userId);
        Task UpdateCartItemQuantityAsync(int cartItemId, int quantity);
    }
}
