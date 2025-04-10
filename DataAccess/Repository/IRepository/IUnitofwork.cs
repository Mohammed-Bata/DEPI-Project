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

        Task SaveAsync();
    }
}
