using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Cart
    {
        public int Id { get; set; }

        [NotMapped]
        public decimal TotalPrice => Items.Sum(item => item.TotalPrice);

        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public string UserId { get; set; }

        public virtual AppUser User { get; set; }
    }
}
