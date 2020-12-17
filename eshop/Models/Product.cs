using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    [Table(nameof(Product))]
    public class Product
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public string DataTarget { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        [Required]
        public string ImageSrc { get; set; }
        [Required]
        public string ImageAlt { get; set; }
        [Required]
        public string ProductInfo { get; set; }


    }
}
