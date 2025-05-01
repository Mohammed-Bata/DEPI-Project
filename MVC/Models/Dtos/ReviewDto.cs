using Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MVC.Models.Dtos
{
    public class ReviewDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Range(1, 5)]
        [Required]
        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public string UserId { get; set; }
        
        public int ProductId { get; set; }
        
    }
}
