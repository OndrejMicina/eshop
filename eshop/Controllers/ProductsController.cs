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

        public async Task<IActionResult> AddToCart(int id)
        {
            //realne nepouzitelne

            string lastOrderIDstring;

            if (!EshopDBContext.Order.Any())
            {
                lastOrderIDstring = "0";
            }
            else lastOrderIDstring = EshopDBContext.Order.OrderByDescending(o => o.OrderNumber).FirstOrDefault().OrderNumber;

            Product productItem = EshopDBContext.Products.Where(carI => carI.ID == id).FirstOrDefault();

            

            if (lastOrderIDstring.Contains("active"))
            {
                Order order = EshopDBContext.Order.Where(o => o.OrderNumber == lastOrderIDstring).FirstOrDefault();

                OrderItem orderItem = new OrderItem
                {
                    OrderID = EshopDBContext.Order.Where(o => o.OrderNumber == lastOrderIDstring).FirstOrDefault().ID,
                    ProductID = id,

                    Amount = 1,
                    Price = EshopDBContext.Products.Where(carI => carI.ID == id).FirstOrDefault().Price,
                    Order = order,
                    Product = productItem
                };

                EshopDBContext.Add(orderItem);
                await EshopDBContext.SaveChangesAsync();
                

            }
            else
            {
                int lastOrderID = Int32.Parse(lastOrderIDstring);
                string newOrderID = (lastOrderID + 1).ToString() + "active";
                Order order = new Order();
                order.OrderNumber = newOrderID;
                EshopDBContext.Add(order);
                await EshopDBContext.SaveChangesAsync();

                OrderItem orderItem = new OrderItem
                {
                    OrderID = EshopDBContext.Order.Where(o => o.OrderNumber == newOrderID).FirstOrDefault().ID,
                    ProductID = id,

                    Amount = 1,
                    Price = EshopDBContext.Products.Where(carI => carI.ID == id).FirstOrDefault().Price,
                    Order = order,
                    Product = productItem
                };

                EshopDBContext.Add(orderItem);
                await EshopDBContext.SaveChangesAsync();
                
            }

            return RedirectToAction(nameof(Detail) + "/"+id.ToString());


            
        }

    }
}
