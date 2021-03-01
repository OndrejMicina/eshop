using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{

    [Table(nameof(CartItem))]
    public class CartItem : Entity
    {
        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }
        [Required]
        public Product CartProduct { get; set; }
        public List<Product> CommonProductsList { get; set; }

    }
}
