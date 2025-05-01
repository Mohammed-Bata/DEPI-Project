using Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MVC.Models
{
    public class Wishlist
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = "Default Wishlist";

        public string UserId { get; set; }
        public ICollection<ProductWishlist> Products { get; set; }
    }
}
