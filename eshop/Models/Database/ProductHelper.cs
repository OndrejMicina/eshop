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
                {

                    Name = "Product1",
                    DataTarget="#myCarousel",
                    Price= 100,
                    ImageSrc ="https://i.ebayimg.com/images/g/SugAAOxyHE5Ro8ab/s-l500.jpg",
                    ImageAlt="ASP.NET",
                    ProductInfo="Learn how to build ASP.NET apps that can run anywhere."


                },
                new Product()
                {
                    Name = "Product2",
                    DataTarget="#myCarousel",
                    Price= 200,
                    ImageSrc ="https://images-na.ssl-images-amazon.com/images/I/517QIzxj77L._SX385_.jpg",
                    ImageAlt="ASP.NET",
                    ProductInfo="Learn how to build ASP.NET apps that can run anywhere."
                },

            };
            return products;
        }
    }
}


