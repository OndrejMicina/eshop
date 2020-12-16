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
         
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Carousel carousel)
        {
            Carousels.Add(carousel);
            return RedirectToAction(nameof(Select));
        }
        public IActionResult Edit(int id)
        {
            Carousel carouselItem = Carousels.Where(carI => carI.ID == id).FirstOrDefault();

            if (carouselItem != null)
            {
                return View(carouselItem);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Edit(Carousel carousel)
        {
            Carousel carouselItem = Carousels.Where(carI => carI.ID == carousel.ID).FirstOrDefault();

            if (carouselItem != null)
            {
                carouselItem.DataTarget = carousel.DataTarget;
                carouselItem.ImageSrc = carousel.ImageSrc;
                carouselItem.ImageAlt = carousel.ImageAlt;
                carouselItem.CarouselContent = carousel.CarouselContent;

                return RedirectToAction(nameof(Select));
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Delete(int id)
        {
            Carousel carouselItem = Carousels.Where(carI => carI.ID == id).FirstOrDefault();

            if (carouselItem != null)
            {
                Carousels.Remove(carouselItem);
                return RedirectToAction(nameof(Select));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
