using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public static class CarouselHelper
    {
        public static IList<Carousel> GenerateCarousel()
        {
            IList<Carousel> carousels = new List<Carousel>()
            {
                new Carousel()
                {   DataTarget="#myCarousel",
                    ImageSrc ="/images/Carousels/tsushima.png",
                    ImageAlt="img",
                    CarouselContent="Ghost of Tsushima"
                },
                new Carousel()
                {   DataTarget="#myCarousel",
                    ImageSrc ="/images/Carousels/img.png",
                    ImageAlt="ASP.NET",
                    CarouselContent="" 
                    
                },
            };
            return carousels;
        }
    }
}
