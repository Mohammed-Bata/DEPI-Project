using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MVC.Models
{
    public class Address
    {
        public int Id { get; set; }
    
        [Required, MaxLength(100)]
        public string Street { get; set; }
        [Required, MaxLength(30)]
        public string City { get; set; }
        [Required, MaxLength(30)]
        public string Governorate { get; set; }
        [Required, MaxLength(10)]
        public string PostalCode { get; set; }
    }
}
