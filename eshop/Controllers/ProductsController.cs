using eshop.Models;
using eshop.Models.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Controllers
{
    public class ProductsController : Controller
    {

        readonly EshopDBContext EshopDBContext;

        public ProductsController(EshopDBContext eshopDBContext)
        {
            this.EshopDBContext = eshopDBContext;
            
        }


        public IActionResult Detail(int id)
        {
            Product productItem = EshopDBContext.Products.Where(carI => carI.ID == id).FirstOrDefault();

            if (productItem != null)
            {
                return View(productItem);
            }
            else
            {
                return NotFound();
            }
        }

        public ActionResult Select()
        {
            return View();
        }
    }
}
