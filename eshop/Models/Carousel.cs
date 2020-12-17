using eshop.Models.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    [Table("Carousel")]
    public class Carousel
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public string DataTarget { get; set; }
        [NotMapped]
        [FileContentTypeAttribute("image")]
        public IFormFile Image { get; set; }
        [Required]
        [StringLength(255)]
        public string ImageSrc { get; set; }
        [Required]
        [StringLength(25)]
        public string ImageAlt { get; set; }
        [Required]
        public string CarouselContent { get; set; }


    }
}
