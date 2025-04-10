using DataAccess.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitofWork
    {
        private readonly AppDbContext _context;

        public AppUserRepository AppUsers { get; private set; }
        public ProductWishlistRepository ProductWishlists { get; private set; }
        public WishlistRepository Wishlists { get; private set; }
        public AddressRepository Addresses { get; private set; }

        public UnitofWork(AppDbContext context, AppUserRepository appUserRepository, ProductWishlistRepository productWishlistRepository, WishlistRepository wishlistRepository, AddressRepository addressRepository)
        {
            _context = context;
            AppUsers = appUserRepository;
            ProductWishlists = productWishlistRepository;
            Wishlists = wishlistRepository;
            Addresses = addressRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
