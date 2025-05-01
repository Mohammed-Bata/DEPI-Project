using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MVC.Models.Dtos
{
    public class ProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        //[Precision(18, 2)]
        public decimal Price { get; set; } = 0;
        //[Precision(18, 2)]

        [Required]
        public int StockAmount { get; set; }

        public int Discount { get; set; } = 0;

        [MaxLength(100)]
        [Required]
        public string Brand { get; set; }

        public int CategoryId { get; set; }

        [FromForm]
        public IFormFile ImageUrl { get; set; }

        //public string ImageLocalPath { get; set; }

        [MaxLength(50)]
        [Required]
        public string ProductCode { get; set; }

    }
}
