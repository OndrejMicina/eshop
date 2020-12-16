using eshop.Models;
using eshop.Models.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        readonly EshopDBContext EshopDBContext;

        public CarouselController(EshopDBContext eshopDBContext, IHostingEnvironment env)
        {
            this.EshopDBContext = eshopDBContext;
            this.Env = env;
        }
        public async Task<IActionResult> Select()
        {
            CarouselViewModel carousel = new CarouselViewModel();
            carousel.Carousels = await EshopDBContext.Carousels.ToListAsync();
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

            EshopDBContext.Carousels.Add(carousel);

            await EshopDBContext.SaveChangesAsync();

            return RedirectToAction(nameof(Select));
        }
        public IActionResult Edit(int id)
        {
            Carousel carouselItem = EshopDBContext.Carousels.Where(carI => carI.ID == id).FirstOrDefault();

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
            Carousel carouselItem = EshopDBContext.Carousels.Where(carI => carI.ID == carousel.ID).FirstOrDefault();

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

                await EshopDBContext.SaveChangesAsync();

                return RedirectToAction(nameof(Select));
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            Carousel carouselItem = EshopDBContext.Carousels.Where(carI => carI.ID == id).FirstOrDefault();

            if (carouselItem != null)
            {
                EshopDBContext.Carousels.Remove(carouselItem);
                await EshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Select));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
