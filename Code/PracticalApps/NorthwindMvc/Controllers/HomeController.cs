using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindMvc.Models;
using　Packt.Shared;
namespace NorthwindMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Northwind db;
        public HomeController(ILogger<HomeController> logger,  Northwind injectedContext)
        {
            _logger = logger;
            db = injectedContext;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel
            {
            VisitorCount = (new Random()).Next(1, 1001), 
            Categories = db.Categories.ToList(), 
            Products = db.Products.ToList()
            };            
            // pass model to View
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ProductDetail(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("You must pass a product ID in the route, for example, /Home/ProductDetail/21");
            }
            // var model = db.Products
            //     .SingleOrDefault(p => p.ProductId == id);
            var model = (from p in db.Products 
                        where p.ProductId == id
                        select p).ToList();
            if (model == null)
            {
                return NotFound($"Product with ID of {id} not found.");
            }
            if(model.Count == 0){
                return NotFound($"Product with ID of {id} not found.");
            }
            else if(model.Count > 1){
                return NotFound($"Product with ID of {id} more than 1.");
            }
            else {
            return View(model.First()); // pass model to view and then return result
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
