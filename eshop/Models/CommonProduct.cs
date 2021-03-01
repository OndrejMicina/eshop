using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    [Table(nameof(CommonProduct))]
    public class CommonProduct : Entity
    {
        [Required]
        public int ProductID { get; set; }
        public int CommonProductID { get; set; }

    }
}
