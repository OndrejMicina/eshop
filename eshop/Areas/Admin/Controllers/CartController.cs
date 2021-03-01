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
            
            if (_context.CartItems.Any())
            {
                List<CartItem> cartItems = new List<CartItem>();
                List<CartItem> cartItemsDb = await _context.CartItems.ToListAsync();
                
                foreach (var cartDbItem in cartItemsDb)
                {
                    //ziskanie listu comon produktov pre dany item kosiku
                    List<CommonProduct> commonProductsList = await _context.CommonProductsDbSet.Where(p => p.ProductID == cartDbItem.ProductID).ToListAsync();

                    List < Product > commonProducts= new List<Product>();

                    //pre kazdy produkt v liste
                    foreach (var prod in commonProductsList)
                    {
                        //pridame objekt produkt do comon produktov
                        commonProducts.Add( _context.Products.Where(p=>p.ID == prod.CommonProductID).FirstOrDefault() );
                    }

                    //vytvorenie cart item
                    var cartItem = new CartItem
                    {
                        CartProduct = _context.Products.Where(p => p.ID == cartDbItem.ProductID).FirstOrDefault(),
                        ProductID = _context.Products.Where(p => p.ID == cartDbItem.ProductID).FirstOrDefault().ID,
                        CommonProductsList = commonProducts,
                    };
                    cartItems.Add(cartItem);
                }

                
                
                return View(cartItems);
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
            /*var orderItem = await _context.OrderItems.FindAsync(id);
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
            if (!_context.OrderItems.Where(o=>o.OrderID==orderItem.OrderID).Any())
            {
                _context.Order.Remove(_context.Order.Where(o=>o.ID == orderItem.OrderID).FirstOrDefault());
            }*/
            return RedirectToAction(nameof(Index));
        }

        /*private bool OrderItemExists(int id)
        {
            return _context.OrderItems.Any(e => e.ID == id);
        }*/

       public async Task<IActionResult> Order()
        {
            if (User.Identity.Name != null)
            {
                if (_context.CartItems.Any())
                {
                    
                    List<CartItem> cartItems = new List<CartItem>();
                    List<CartItem> cartItemsDb = await _context.CartItems.ToListAsync();

                    //ziskanie z kosika
                    foreach (var item in cartItemsDb)
                    {
                        var cartItem = new CartItem
                        {
                            CartProduct = _context.Products.Where(p => p.ID == item.ProductID).FirstOrDefault(),
                            ProductID = _context.Products.Where(p => p.ID == item.ProductID).FirstOrDefault().ID,

                        };

                        cartItems.Add(cartItem);
                        _context.CartItems.Remove(item);

                    }
                    await _context.SaveChangesAsync();

                    // pridanie do comon items
                    //pre kazdy produkt v kosiku
                    foreach (var cartItm in cartItems)
                    {                        
                        
                        //pripiseme kazdu polozku z kosiku ako common
                        foreach (var item in cartItems)
                        {
                            //ochrana pred pridanim sameho seba ako common
                            if (cartItm!=item)
                            {
                                CommonProduct commonProduct = new CommonProduct { ProductID = cartItm.ProductID, CommonProductID = item.CartProduct.ID };
                                if (!_context.CommonProductsDbSet.Any(cp=> cp.ProductID == cartItm.ProductID && cp.CommonProductID == item.CartProduct.ID ))
                                {
                                    _context.CommonProductsDbSet.Add(commonProduct);
                                }                                

                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                    await _context.SaveChangesAsync();
                    var pp = _context;

                    //vytvorenie objednavky
                    Order order;
                    if (!_context.Order.Any())
                    {
                        order = new Order
                        {
                            OrderNumber = "0",
                            UserName = User.Identity.Name,
                            OrderItems = new List<OrderItem>()
                        };
                    }
                    else
                    {
                        order = new Order
                        {
                            OrderNumber = (UInt32.Parse(_context.Order.Last().OrderNumber) + 1).ToString(),
                            UserName = User.Identity.Name,
                            OrderItems = new List<OrderItem>()
                        };
                    }

                    _context.Order.Add(order);
                    await _context.SaveChangesAsync();

                    //vytvorenie orderItems
                    IList<OrderItem> orderItemsList = new List<OrderItem>();

                    foreach (var item in cartItems)
                    {
                        OrderItem orderItem = new OrderItem
                        {
                            Order = _context.Order.Where(o => o.ID == order.ID).FirstOrDefault(),
                            Amount = 1,
                            Price = item.CartProduct.Price,
                            ProductID = item.CartProduct.ID,
                            OrderID = _context.Order.Where(o => o.ID == order.ID).FirstOrDefault().ID,
                            Product = item.CartProduct,
                        };

                        orderItemsList.Add(orderItem);
                    }

                    //pridanie orderItems do objednavky
                    Order editingOrder = _context.Order.Where(o => o.ID == order.ID).FirstOrDefault();
                    editingOrder.OrderItems = orderItemsList;
                    await _context.SaveChangesAsync();
                }
            }
                        


            return View(nameof(Index));
        }



    }
}
