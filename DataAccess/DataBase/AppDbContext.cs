using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataBase
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
                
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<ProductWishlist> ProductWishlists { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductWishlist>()
                .HasKey(pw => new { pw.ProductId, pw.WishlistId });
        }
    }
}
