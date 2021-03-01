using eshop.Models;
using eshop.Models.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        IHostingEnvironment Env;
        readonly EshopDBContext EshopDBContext;

        public ProductController(EshopDBContext eshopDBContext, IHostingEnvironment env)
        {
            this.EshopDBContext = eshopDBContext;
            this.Env = env;
        }
        public async Task<IActionResult> Index()
        {
            ProductViewModel product = new ProductViewModel();
            product.Products = await EshopDBContext.Products.ToListAsync();
            return View(product);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {

            if (ModelState.IsValid)
            {
                product.ImageSrc = String.Empty;

                FileUpload fup = new FileUpload(Env.WebRootPath, "Products", "image");
                product.ImageSrc = await fup.FileUploadAsync(product.Image);

                //mozno nefunguje - v tom pripade nastavit az pri vytvarani objednavky

                EshopDBContext.Products.Add(product);

                await EshopDBContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(product);
            }

        }

        // GET: ProductController/Edit/5
        public IActionResult Edit(int id)
        {
            Product productItem = EshopDBContext.Products.Where(prodI => prodI.ID == id).FirstOrDefault();

            if (productItem != null)
            {
                return View(productItem);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            Product productItem = EshopDBContext.Products.Where(prodI => prodI.ID == product.ID).FirstOrDefault();

            if (productItem != null)
            {
                productItem.DataTarget = product.DataTarget;
                productItem.Name = product.Name;
                productItem.Price = product.Price;
                productItem.ImageAlt = product.ImageAlt;
                productItem.ProductInfo = product.ProductInfo;
                productItem.DetailInfo = product.DetailInfo;

                FileUpload fup = new FileUpload(Env.WebRootPath, "Products", "image");
                if (String.IsNullOrWhiteSpace(product.ImageSrc = await fup.FileUploadAsync(product.Image)) == false)
                {
                    productItem.ImageSrc = product.ImageSrc;
                }

                await EshopDBContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        // GET: ProductController/Delete/5

        public async Task<IActionResult> Delete(int id)
        {
            Product productItem = EshopDBContext.Products.Where(carI => carI.ID == id).FirstOrDefault();

            if (productItem != null)
            {
                EshopDBContext.Products.Remove(productItem);
                await EshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
