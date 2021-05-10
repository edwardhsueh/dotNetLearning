using Microsoft.AspNetCore.Mvc.RazorPages; 
using System.Collections.Generic;
using System.Linq; 
using Packt.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace NorthwindWeb.Pages
{
// SuppliersModel inherits from PageModel, so it has members such as the ViewData dictionary for sharing data. Y
// SuppliersModel defines a property for storing a collection of string values named Suppliers.
// When an HTTP GET request is made for this Razor Page, the Suppliers property is populated with some example supplier names.    
  public class SuppliersModel : PageModel
  {
    private Northwind db;
    // constructor to get database context
    public SuppliersModel(Northwind injectedContext)
    {
        db = injectedContext;
    }
    public IEnumerable<Supplier> Suppliers { get; set; } 
    public void OnGet()
    {
      ViewData["Title"] = "Northwind Web Site - Suppliers"; 
    //   Suppliers = new[] { 
    //     "Alpha Co", "Beta Limited", "Gamma Corp"
    //   };
      // Suppliers = db.Suppliers.Select(s => s.CompanyName);    
      Suppliers = from s in db.Suppliers  
                  select s;
    }


    // added a property named Supplier 
    // that is decorated with the [BindProperty] attribute 
    // so that we can easily connect HTML elements 
    // on the web page to properties in the Supplier class
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-5.0
    // will search Supplier in form
    [BindProperty(SupportsGet = true)]
    public Supplier Supplier { get; set; }
    // added a method that responds to HTTP POST requests. 
    // It checks that all property values conform to validation rules and then adds the supplier to the existing table and saves changes to the database context. This will generate a SQL statement to perform the insert into the database. 
    // Then it redirects to the Suppliers page so that the visitor sees the newly added supplier.
    public IActionResult OnPost()
    {
        // checks that all property values conform to validation rules
        if (ModelState.IsValid)
        {
            var loggerFactory = db.GetService<ILoggerFactory>();
            loggerFactory.AddProvider(new ConsoleLoggerProvider());  
            Console.WriteLine("SupplierId/Name:{0}/{1}", Supplier.SupplierId, Supplier.CompanyName);
            // saves changes to the database context
            db.Suppliers.Add(Supplier);
            db.SaveChanges();
            // it redirects to the Suppliers page so that the visitor sees the newly added supplier.
            return RedirectToPage("/suppliers");
        }
        return Page();
    }

  }
}