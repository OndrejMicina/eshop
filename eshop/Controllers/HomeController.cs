using eshop.Models;
using eshop.Models.Database;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Controllers
{
    public class HomeController : Controller
    {
        readonly EshopDBContext EshopDBContext;
        readonly ILogger<HomeController> logger;
        private IList<Product> products;
        
        public HomeController (EshopDBContext eshopDBContext, ILogger<HomeController> logger)
        {
            this.EshopDBContext = eshopDBContext;
            this.logger = logger;
        }

        public async  Task<IActionResult> Index()
        {
            products = await EshopDBContext.Products.ToListAsync();
            CarouselViewModel carousel = new CarouselViewModel();
            carousel.Carousels = await EshopDBContext.Carousels.ToListAsync();
            ViewData["Products"] = products;
            return View(carousel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            //throw new Exception("neco se stalo");
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var featureException = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            this.logger.LogError("Exception occured: " + featureException.Error.ToString() + Environment.NewLine + "Exception Path: " +featureException.Path);

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ErrorCodeStatus(int? statusCode = null)
        {
            string originalURL = String.Empty;
            var features = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if (features != null)
            {
                originalURL = features.OriginalPathBase + features.OriginalPath + features.OriginalQueryString;
            }

            var statCode = statusCode.HasValue ? statusCode.Value : 0;

            this.logger.LogWarning("Status Code: " + statusCode + " Original URL: "+ originalURL);

            if (statCode == 404)
            {
                _404ViewModel vm404 = new _404ViewModel()
                {
                    StatusCode = statCode
                };
                return View(statusCode.ToString(), vm404);
            }

            ErrorCodeStatusViewModel vm = new ErrorCodeStatusViewModel()
            {
                StatusCode = statCode,
                OriginalURL = originalURL
            };
            return View(vm);
        }
    }
}
