using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitofwork
    {
        public AppUserRepository AppUsers { get; }
        public ProductWishlistRepository ProductWishlists { get; }
        public WishlistRepository Wishlists { get; }
        public AddressRepository Addresses { get; }
        ICategoryRepository CategoryRepository { get; }
        IOrderRepository Orders { get; }
        IOrderItemRepository OrderItems { get; }
        public ProductRepository Products { get; }
        ICartRepository Carts { get; }
        public ReviewRepository Reviews { get; }

        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        Task SaveAsync();
    }
}
