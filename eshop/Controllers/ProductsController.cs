using eshop.Models;
using eshop.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> AddToCart(int id)
        {

            var cartItem = new CartItem
            {
                CartProduct = EshopDBContext.Products.Where(carI => carI.ID == id).FirstOrDefault()
            };

            await EshopDBContext.CartItems.AddAsync(cartItem);
            //EshopDBContext.Entry(cartItem).State = EntityState.Added;

            await EshopDBContext.SaveChangesAsync();            

            return Redirect("https://localhost:44381/Admin/Cart");
            //return RedirectToAction(nameof(Detail));




        }

    }
}
