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
            if (dbContext.Database.EnsureCreated())
            {
                IList<Carousel> carousels = CarouselHelper.GenerateCarousel();
                foreach (var item in carousels)
                {
                    dbContext.Carousels.Add(item);
                }
                dbContext.SaveChangesAsync();
            }
        }
    }
}
