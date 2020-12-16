using eshop.Models;
using eshop.Models.DatabaseFake;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarouselController : Controller
    {
        IHostingEnvironment Env;
        IList<Carousel> Carousels = DatabaseFake.Carousels;
        public CarouselController(IHostingEnvironment env)
        {
            this.Env = env;
        }
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
        public async Task<IActionResult> Create(Carousel carousel)
        {
            carousel.ImageSrc = String.Empty;

            FileUpload fup = new FileUpload(Env);
            await fup.FileUploadAsync(carousel); 

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
        public async Task<IActionResult> Edit(Carousel carousel)
        {
            Carousel carouselItem = Carousels.Where(carI => carI.ID == carousel.ID).FirstOrDefault();

            if (carouselItem != null)
            {
                carouselItem.DataTarget = carousel.DataTarget;
                carouselItem.ImageAlt = carousel.ImageAlt;
                carouselItem.CarouselContent = carousel.CarouselContent;

                FileUpload fup = new FileUpload(Env);
                if (await fup.FileUploadAsync(carousel))
                {
                    carouselItem.ImageSrc = carousel.ImageSrc;
                }
                

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
