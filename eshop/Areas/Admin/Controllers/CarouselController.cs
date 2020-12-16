using eshop.Models;
using eshop.Models.DatabaseFake;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarouselController : Controller
    {
        IList<Carousel> Carousels = DatabaseFake.Carousels;
        public IActionResult Select()
        {
            CarouselViewModel carousel = new CarouselViewModel();
            carousel.Carousels = Carousels;
            return View(carousel);
        }
    }
}
