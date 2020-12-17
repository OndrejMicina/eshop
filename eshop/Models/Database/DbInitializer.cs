using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public static class DbInitializer
    {
        public static void Initialize(EshopDBContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Carousels.Count()==0)
            {
                IList<Carousel> carousels = CarouselHelper.GenerateCarousel();
                foreach (var item in carousels)
                {
                    dbContext.Carousels.Add(item);
                }
                dbContext.SaveChanges();                
            }
            if (dbContext.Products.Count()==0)
            {
                IList<Product> products = ProductHelper.GenerateCarousel();
                foreach (var item in products)
                {
                    dbContext.Products.Add(item);
                }
                dbContext.SaveChanges();
            }
        }
    }
}
