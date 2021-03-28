using System;
using static System.Console;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;

namespace WorkingWithEFCore
{
    class Program
    {
        /// <summary>
        /// using LINQ query expression
        /// </summary>
        /// <value></value>
        static void tryJoin(){
            using (var db = new Northwind()) // using is important
            {
                var query = from cat in db.Categories
                            join prod in db.Products on cat.CategoryID equals prod.CategoryID
                            where cat.CategoryName == "Beverageshas"
                            select new {cat, prod};
                WriteLine("Inner Query String:\n"+query.ToQueryString());
                var query2 = from cat in db.Categories
                            join prod in db.Products on cat.CategoryID equals prod.CategoryID into prodGroup
                            from subProd in prodGroup.DefaultIfEmpty()
                            select new {cat, subProd};
                WriteLine("Left Join Query String:\n"+query2.ToQueryString());
                var query3 = from cat in db.Categories
                            from prod in db.Products
                            select new {cat, prod};
                WriteLine("Cross Join Query String:\n"+query3.ToQueryString());
            }
        }
        /// <summary>
        /// using Query Expression
        /// </summary>
        static void QueryingCategoriesQE()
        {
            using (var db = new Northwind()) // using is important
            {
                WriteLine("Categories and how many products they have:");
                // a query to get all categories and their related products

                // database Evaluation
                var query = from cat in db.Categories
                            join prod in db.Products on cat.CategoryID equals prod.CategoryID into prodGroup
                            from subProd in prodGroup.DefaultIfEmpty()
                            group subProd by cat.CategoryName into g
                            select new {CategoryName = g.Key, ProductCount = g.Count()};
                WriteLine("Left Join Query String:\n\n"+query.ToQueryString());
                foreach(var qr in query){
                    WriteLine("Category:{0} has {1} products", qr.CategoryName, qr.ProductCount);
                }

            }
        }


        static void QueryingCategories()
        {
            using (var db = new Northwind()) // using is important
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());
                WriteLine("Categories and how many products they have:");
                // a query to get all categories and their related products
                IQueryable<Category> cats = db.Categories.TagWith("QueryingCategories").Include(c => c.Products);
                // WriteLine(cats.ToQueryString());
                foreach (Category c in cats)
                {
                    WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
                }
            }
        }
        /// <summary>
        /// Left Join using LINQ Expression to update into Entities Property
        /// Afterwards, using client Group to get Grouped information like Product Count or Product total Stock
        /// with the same category
        /// </summary>
        static void FilteredIncludesQE()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());
                Write("Enter a minimum for units in stock: ");
                string unitsInStock = ReadLine();
                int stock = int.Parse(unitsInStock);
                var prodQuery = from prod in db.Products
                                where prod.Stock > stock
                                select prod;
                // Query that will update Category and Category Entries
                var query = from cat in db.Categories
                            join prod in prodQuery
                            on cat.CategoryID equals prod.CategoryID into prodGroup
                            from subProd in prodGroup.DefaultIfEmpty()
                            orderby cat.CategoryID ascending, subProd.ProductID ascending
                            select new {cat, subProd};
                // WriteLine($"** ToQueryString: {query.ToQueryString()}");
                // Execute Query
                var qResult = query.TagWith("FilteredIncludesQE").ToList();
                foreach (var qr in qResult) {
                    // var prodName = (qr.subProd == null) ? "Nothing" : qr.subProd.ProductName;
                    var prodName = qr.subProd?.ProductName ?? "Nothing";
                    var TotalProduct = qr.cat.Products.Count;
                    WriteLine(qr.cat.CategoryName + "___" + prodName + "___" + TotalProduct);
                }
                var grQuery = from q in qResult
                              group q by q.cat;
                foreach (var gr in grQuery){
                    WriteLine($"{gr.Key.CategoryName} has {gr.Key.Products.Count} products with a minimum of {stock} units in stock.");
                    WriteLine($"{gr.Key.CategoryName} has {gr.Key.Products.Sum(p => p.Stock)} Stocks with a minimum of {stock} units in stock.");
                    foreach(var prod in gr.Key.Products){
                        WriteLine($" {prod.ProductName} has {prod.Stock} units in stock.");
                    }
                }
            }
        }
        /// <summary>
        /// InterJoin using LINQ expression and Using Database Group By
        /// </summary>
        static void FilteredIncludesQE2()
        {
            using (var db = new Northwind())
            {
               var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());
                Write("Enter a minimum for units in stock: ");
                string unitsInStock = ReadLine();
                int stock = int.Parse(unitsInStock);
                var prodQuery = from prod in db.Products
                                where prod.Stock > stock
                                select prod;
                var query = from cat in db.Categories
                            join prod in prodQuery
                            on cat.CategoryID equals prod.CategoryID
                            orderby cat.CategoryID ascending, prod.ProductID ascending
                            group prod by cat.CategoryName into gr
                            select new {CategoryID = gr.Key, TotalStock = gr.Sum(p => p.Stock), AverageCost = gr.Average(p => p.Stock)};
                WriteLine($"** ToQueryString: {query.ToQueryString()}");

                var queryResult = query.TagWith("FilteredIncludesQE2").ToList();
                foreach(var qr in queryResult){
                    WriteLine("Name:{0}, SumStock:{1}, AvgCost:{2}", qr.CategoryID, qr.TotalStock, qr.AverageCost);
                }
            }
        }
        /// <summary>
        /// LeftJoin using LINQ expression and Using Database Group By
        /// </summary>
        static void FilteredIncludesQE3()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());
                Write("Enter a minimum for units in stock: ");
                string unitsInStock = ReadLine();
                int stock = int.Parse(unitsInStock);
                var prodQuery = from prod in db.Products
                                where prod.Stock > stock
                                select prod;
                var query = from cat in db.Categories
                            join prod in prodQuery on cat.CategoryID equals prod.CategoryID into prodGroup
                            from subProd in prodGroup.DefaultIfEmpty()
                            orderby cat.CategoryID ascending, subProd.ProductID ascending
                            group subProd by cat.CategoryName into gr
                            select new {CategoryID = gr.Key, TotalProduct=gr.Count(x => x!=null), TotalStock = gr.Sum(p => p.Stock), AverageCost = gr.Average(p => p.Stock)};
                WriteLine($"** ToQueryString: {query.ToQueryString()}");

                var queryResult = query.TagWith("FilteredIncludesQE3").ToList();
                foreach(var qr in queryResult){
                    WriteLine("Name:{0}, SumStock:{1}, AvgCost:{2}, TotalProduct:{3}", qr.CategoryID, qr.TotalStock, qr.AverageCost, qr.TotalProduct);
                }
                // var query2 = from cat in db.Categories
                //             join prod in prodQuery
                //             on cat.CategoryID equals prod.CategoryID
                //             orderby cat.CategoryID ascending, prod.ProductID ascending
                //             select new {Category = cat, Product = prod} into x
                //             group x by x.Category.CategoryID into gr
                //             select new {CategoryID = gr.Key, TotalCost = gr.Product.Stock.Sum()};
                // WriteLine($"** ToQueryString: {query2.ToQueryString()}");

                // var qResult = query.ToList();
                // foreach (var qr in qResult) {
                //     var prodName = qr.prod?.ProductName ?? "Nothing";
                //     WriteLine(qr.cat.CategoryName + "___" + prodName);
                // }
                // var grQuery = from q in qResult
                //               group q by q.cat.CategoryName;
                // if(grQuery.Count() > 0){
                //     foreach (var gr in grQuery){
                //         foreach(var grVal in gr){
                //             WriteLine($"{gr.Key} has {grVal.cat.Products.Count} products with a minimum of {stock} units in stock.");
                //             foreach (var p in grVal.cat.Products){
                //                  WriteLine($" {p.ProductName} has {p.Stock} units in stock.");
                //             }
                //         }
                //     }
                // }
                // else {
                //     WriteLine($"No one has products with a minimum of {stock} in stock");
                // }
            }
        }

        /// <summary>
        /// Left Join using Method
        /// </summary>
        static void FilteredIncludes()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());
                Write("Enter a minimum for units in stock: ");
                string unitsInStock = ReadLine();
                int stock = int.Parse(unitsInStock);
                IQueryable<Category> cats = db.Categories.TagWith("FilteredIncludes")
                .Include(c => c.Products.Where(p => p.Stock >= stock));
                // WriteLine($"ToQueryString: {cats.ToQueryString()}");
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
            string file = Path.Combine(Environment.CurrentDirectory, "db.log");
            using(var fs = File.CreateText(file)){
                fs.WriteLine("** Created on: "+ DateTime.Now.ToString());
            }

            // tryJoin();
            QueryingCategories();
            // QueryingCategoriesQE();
            FilteredIncludes();
            FilteredIncludesQE();
            FilteredIncludesQE2();
            FilteredIncludesQE3();
            // QueryingProducts();
        }
    }
}
