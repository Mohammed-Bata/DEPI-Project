using Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace API.DTOs
{
    public class ReviewUpdateDto
    {
        [Required]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Range(1, 5)]
        [Required]
        public int Rating { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int ProductId { get; set; }
    }
}
