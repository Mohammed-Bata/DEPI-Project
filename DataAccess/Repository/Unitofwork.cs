using DataAccess.DataBase;
using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitofWork:IUnitofwork
    {
        private readonly AppDbContext _context;

        public AppUserRepository AppUsers { get; private set; }
        public ProductWishlistRepository ProductWishlists { get; private set; }
        public WishlistRepository Wishlists { get; private set; }
        public AddressRepository Addresses { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IOrderItemRepository OrderItems { get; private set; }

        public UnitofWork(AppDbContext context, AppUserRepository appUserRepository, ProductWishlistRepository productWishlistRepository, WishlistRepository wishlistRepository, AddressRepository addressRepository,IOrderRepository orderRepository,
			ICategoryRepository categoryRepository)
        {
            _context = context;
            AppUsers = appUserRepository;
            ProductWishlists = productWishlistRepository;
            Wishlists = wishlistRepository;
            Addresses = addressRepository;
            Orders = orderRepository;
            CategoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await Orders.GetOrdersByUserIdAsync(userId);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
