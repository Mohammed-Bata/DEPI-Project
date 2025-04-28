using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.DTOs
{
    public class ProductInsertDto
    {
        

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; } = 0;
        [Precision(18, 2)]

        [Required]
        public int StockAmount { get; set; }

        public int Discount { get; set; } = 0;

        [MaxLength(100)]
        [Required]
        public string Brand { get; set; }

        public int CategoryId { get; set; }

        [FromForm]
        public IFormFile ImageUrl { get; set; }

        public string ImageLocalPath { get; set; }

        [MaxLength(50)]
        [Required]
        public string ProductCode { get; set; }







    }
}
