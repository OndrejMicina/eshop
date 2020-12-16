using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public class EshopDBContext : DbContext
    {
        public EshopDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Carousel> Carousels { get; set; }
        //public DbSet<Product> Products { get; set; }
    }
}
