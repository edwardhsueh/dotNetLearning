using System;
using static System.Console;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace WorkingWithEFCore
{
    class Program
    {
        static void QueryingCategories()
        {
            using (var db = new Northwind()) // using is important
            {
                WriteLine("Categories and how many products they have:");
                // a query to get all categories and their related products
                IQueryable<Category> cats = db.Categories.Include(c => c.Products);
                foreach (Category c in cats)
                {
                    WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
                }
            }
        }
        static void FilteredIncludes()
        {
            using (var db = new Northwind())
            {
                Write("Enter a minimum for units in stock: ");
                string unitsInStock = ReadLine();
                int stock = int.Parse(unitsInStock);
                IQueryable<Category> cats = db.Categories
                .Include(c => c.Products.Where(p => p.Stock >= stock));
                WriteLine($"ToQueryString: {cats.ToQueryString()}");
                foreach (Category c in cats)
                {
                    WriteLine($"{c.CategoryName} has {c.Products.Count} products with a minimum of {stock} units in stock.");
                foreach(Product p in c.Products)
                {
                    WriteLine($" {p.ProductName} has {p.Stock} units in stock.");
                }
                }
            }
        }
        static void QueryingProducts()
        {
            using (var db = new Northwind())
            {
                WriteLine("Products that cost more than a price, highest at top.");
                string input;
                decimal price;
                do
                {
                    Write("Enter a product price: ");
                    input = ReadLine();
                } while(!decimal.TryParse(input, out price));
                IQueryable<Product> prods = db.Products
                    .Where(product => product.Cost > price)
                    .OrderByDescending(product => product.Cost);
                foreach (Product item in prods)
                {
                    WriteLine(
                    "{0}: {1} costs {2:$#,##0.00} and has {3} in stock.",
                    item.ProductID, item.ProductName, item.Cost, item.Stock);
                }
            }
        }
        static void Main(string[] args)
        {
            QueryingCategories();
            FilteredIncludes();
            // QueryingProducts();
        }
    }
}
