using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public static class ProductHelper
    {
        public static IList<Product> GenerateProducts()
        {
            IList<Product> products = new List<Product>()
            {
                new Product()
                {   DataTarget="#myCarousel",
                    ImageSrc ="/images/banner1.svg",
                    ImageAlt="ASP.NET",
                    ProductInfo="Learn how to build ASP.NET apps that can run anywhere."
                },
                new Product()
                {   DataTarget="#myCarousel",
                    ImageSrc ="/images/banner1.svg",
                    ImageAlt="ASP.NET",
                    ProductInfo="Learn how to build ASP.NET apps that can run anywhere."
                },

            };
            return products;
        }
    }
}


