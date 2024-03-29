﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NorthwindMvc.Models;
using　Packt.Shared;
using System.Net.Http;
using System.Net.Http.Json;
namespace NorthwindMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // injected database to Controller
        private Northwind db;
        private readonly IHttpClientFactory clientFactory;
        public HomeController(ILogger<HomeController> logger,  Northwind injectedContext, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            db = injectedContext;
            clientFactory = httpClientFactory;
        }

        // for Home/Index
        // public IActionResult Index()
        // {
        //     _logger.LogInformation("Index Page");
        //     var model = new HomeIndexViewModel
        //     {
        //         VisitorCount = (new Random()).Next(1, 1001), 
        //         Categories = db.Categories.ToList(), 
        //         Products = db.Products.ToList()
        //     };            
        //     // pass model to View
        //     return View(model);
        // }
        // Improving scalability using asynchronous tasks
        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexViewModel
            {
                VisitorCount = (new Random()).Next(1, 1001),
                Categories = await db.Categories.ToListAsync(),
                Products = await db.Products.ToListAsync()
            };
            return View(model); // pass model to view
        }
        public IActionResult Privacy()
        {
            return View();
        }

        // Improving scalability using asynchronous tasks
        public async Task<IActionResult> ProductDetail(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("You must pass a product ID in the route, for example, /Home/ProductDetail/21");
            }
            // var model = db.Products
            //     .SingleOrDefault(p => p.ProductId == id);
            var model = await (from p in db.Products 
                        where p.ProductId == id
                        select p).ToListAsync();
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

        // public IActionResult ProductDetail(int? id)
        // {
        //     if (!id.HasValue)
        //     {
        //         return NotFound("You must pass a product ID in the route, for example, /Home/ProductDetail/21");
        //     }
        //     // var model = db.Products
        //     //     .SingleOrDefault(p => p.ProductId == id);
        //     var model = (from p in db.Products 
        //                 where p.ProductId == id
        //                 select p).ToList();
        //     if (model == null)
        //     {
        //         return NotFound($"Product with ID of {id} not found.");
        //     }
        //     if(model.Count == 0){
        //         return NotFound($"Product with ID of {id} not found.");
        //     }
        //     else if(model.Count > 1){
        //         return NotFound($"Product with ID of {id} more than 1.");
        //     }
        //     else {
        //     return View(model.First()); // pass model to view and then return result
        //     }
        // }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ModelBinding()
        {
            return View(); // the page with a form to submit
        }
        [HttpPost]
        public IActionResult ModelBinding(Thing thing)
        {
            // return View(thing); // show the model bound thing, from Posted Data
// The process of model binding can cause errors, for example, data type conversions or validation errors if the model has been decorated with validation rules. What data has been bound and any binding or validation errors are stored in ControllerBase.ModelState            
            var model = new HomeModelBindingViewModel
            {
                Thing = thing,
                HasErrors = !ModelState.IsValid,
                ValidationErrors = ModelState.Values
                .SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage)
            };
            return View(model);
        }       
        public IActionResult ProductsThatCostMoreThan(decimal? price)
        {
            if (!price.HasValue)
            {
                return NotFound("You must pass a product price in the query string, for example, /Home/ProductsThatCostMoreThan?price=50");
            }
            // IEnumerable<Product> model = db.Products
            //     .Include(p => p.Category)
            //     .Include(p => p.Supplier)
            //     .Where(p => p.UnitPrice > price);

            var model = (from p in db.Products
                        where p.UnitPrice > price
                        join c in db.Categories on p.CategoryId equals c.CategoryId
                        join s in db.Suppliers on p.SupplierId equals s.SupplierId
                        select p).ToList();

            // // force include Category      
            // var m_lj_c = from m in model
            //              join c in db.Categories on m.CategoryId equals c.CategoryId into pcGroup
            //              from pc in pcGroup.DefaultIfEmpty()
            //              select pc;
            // m_lj_c.ToList();                   
            // // force include Supplier      
            // var m_lj_s = from m in model
            //              join s in db.Suppliers on m.SupplierId equals s.SupplierId into psGroup
            //              from ps in psGroup.DefaultIfEmpty()
            //              select ps;
            // m_lj_s.ToList();                   
            // force include Category and Supplier
            // force include Supplier      
            if (model.Count() == 0)
            {
                return NotFound(
                $"No products cost more than {price:C}.");
            }
            ViewData["MaxPrice"] = price.Value.ToString("C");
            return View(model); // pass model to view
        }         
        public async Task<IActionResult> Customers(string country)
        {
            string uri;
            if (string.IsNullOrEmpty(country))
            {
                ViewData["Title"] = "All Customers Worldwide";
                uri = "api/customers/";
            }
            else
            {
                ViewData["Title"] = $"Customers in {country}";
                uri = $"api/customers/?country={country}";
            }
            var client = clientFactory.CreateClient(
                name: "NorthwindService");
            var request = new HttpRequestMessage(
                method: HttpMethod.Get, requestUri: uri);
            HttpResponseMessage response = await client.SendAsync(request);
            var model = await response.Content
                .ReadFromJsonAsync<IEnumerable<Customer>>();
            return View(model);
        }        
    }
}
