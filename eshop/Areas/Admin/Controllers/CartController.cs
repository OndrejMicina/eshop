using eshop.Models;
using eshop.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CartController : Controller
    {
        private readonly EshopDBContext _context;

        public CartController(EshopDBContext context)
        {
            _context = context;
        }

        // GET: Admin/OrderItems
        public async Task<IActionResult> Index()
        {           
            
            if (_context.Order.Any(o => o.OrderNumber.Contains("active")))
            {
                int activeOrderID = Int32.Parse(_context.Order.Where(o => o.OrderNumber.Contains("active")).LastOrDefault().OrderNumber.Replace("active",""));
                var eshopDBContext = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).Where(o => o.OrderID == activeOrderID);
                return View(await eshopDBContext.ToListAsync());
            }
            else return View();
        }

        /*public async Task<IActionResult> Delete()
        {

            if (_context.Order.Any(o => o.OrderNumber.Contains("active")))
            {
                int activeOrderID = Int32.Parse(_context.Order.Where(o => o.OrderNumber.Contains("active")).LastOrDefault().OrderNumber);
                var eshopDBContext = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).Where(o => o.OrderID == activeOrderID);
                return View(await eshopDBContext.ToListAsync());
            }
            else return View();



            //int activeOrderID = Int32.Parse(_context.Order.Where(o => o.OrderNumber.Contains("active")).LastOrDefault().OrderNumber);

            //var eshopDBContext = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).Where(o => o.OrderID == activeOrderID);
            //return View(await eshopDBContext.ToListAsync());
        }*/

        public async Task<IActionResult> Delete(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
            if (!_context.OrderItems.Where(o=>o.OrderID==orderItem.OrderID).Any())
            {
                _context.Order.Remove(_context.Order.Where(o=>o.ID == orderItem.OrderID).FirstOrDefault());
            }
            return RedirectToAction(nameof(Index));
        }

        /*private bool OrderItemExists(int id)
        {
            return _context.OrderItems.Any(e => e.ID == id);
        }*/

       public async Task<IActionResult> Order()
        {
            Order activeOrder = _context.Order.Where(o => o.OrderNumber.Contains("active")).LastOrDefault();
            activeOrder.OrderNumber = activeOrder.OrderNumber.Replace("active", "");
            _context.Update(activeOrder);
            await _context.SaveChangesAsync();              
            return RedirectToAction(nameof(Index));
        }



    }
}
