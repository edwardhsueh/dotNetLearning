﻿using System;
using static System.Console;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization; // XmlSerializer
using System.Collections.Generic;
using System.Xml.Linq;
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
                            select new {Cat = cat, Prod = subProd};
                // WriteLine($"** ToQueryString: {query.ToQueryString()}");
                // Execute Query
                var qResult = query.TagWith("FilteredIncludesQE").ToList();
                foreach (var qr in qResult) {
                    // var prodName = (qr.subProd == null) ? "Nothing" : qr.subProd.ProductName;
                    var prodName = qr.Prod?.ProductName ?? "Nothing";
                    var TotalProduct = qr.Cat.Products.Count;
                    WriteLine(qr.Cat.CategoryName + "___" + prodName + "___" + TotalProduct);
                }
                var grQuery = from q in qResult
                              group q by q.Cat;
                foreach (var gr in grQuery){
                    WriteLine($"{gr.Key.CategoryName} has {gr.Count(x => x.Prod != null)} products with a minimum of {stock} units in stock.");
                    WriteLine($"{gr.Key.CategoryName} Total Stock is {gr.Sum(x => x.Prod?.Stock ?? 0)}");
                    WriteLine($"{gr.Key.CategoryName} Average Cost is {gr.Average(x => x.Prod?.Cost ?? 0)}");
                    foreach(var prod in gr.Key.Products){
                        WriteLine($" {prod.ProductName} has {prod.Stock} units in stock and cost {prod.Cost}");
                    }
                }
                // foreach (var gr in grQuery){
                //     WriteLine($"{gr.Key.CategoryName} has {gr.Key.Products.Count} products with a minimum of {stock} units in stock.");
                //     WriteLine($"{gr.Key.CategoryName} has {gr.Key.Products.Sum(p => p.Stock)} Stocks with a minimum of {stock} units in stock.");
                //     foreach(var prod in gr.Key.Products){
                //         WriteLine($" {prod.ProductName} has {prod.Stock} units in stock.");
                //     }
                // }
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
        /// LeftJoin using LINQ expression and Using Database Group By
        /// Database Group by must result in a literal value
        /// </summary>
        static void FilteredIncludesQE5()
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
                            group new {Cat = cat, Prod = subProd} by cat.CategoryName into gr
                            select new QE5Result {CategoryID = gr.Key, TotalProduct=gr.Count(x => x.Prod !=null), TotalStock = gr.Sum(gr => gr.Prod.Stock), AverageCost = gr.Average(gr => gr.Prod.Stock)};
                WriteLine($"** ToQueryString: {query.ToQueryString()}");

                var queryResult = query.TagWith("FilteredIncludesQE5").ToList();
                // JSON serialization
                var jsonString =  JsonSerializer.Serialize(queryResult);
                File.WriteAllText("FilteredIncludesQE5.json", jsonString);

                var xs = new XmlSerializer(typeof(List<QE5Result>));
                TextWriter writer = new StreamWriter("FilteredIncludesQE5.xml");
                xs.Serialize(writer, queryResult);

                var xml = new XElement("products",
                    from p in queryResult
                    select new XElement("product",
                        new XAttribute("id", p.CategoryID),
                        new XElement("TotalProduct", p.TotalProduct),
                        new XElement("AvrageCost", p.AverageCost)
                    )
                );
                // WriteLine(xml.ToString());
                File.WriteAllText("FilteredIncludesQE5_LINQ.xml", xml.ToString());





                foreach(var qr in queryResult){
                    WriteLine("Name:{0}, SumStock:{1}, AvgCost:{2}, TotalProduct:{3}", qr.CategoryID, qr.TotalStock, qr.AverageCost, qr.TotalProduct);
                }
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
        static void QueryingWithLike()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());
                Write("Enter part of a product name: ");
                string input = ReadLine();
                // SQL Like: The percent sign (%) represents zero, one, or multiple characters
                IQueryable<Product> prods = db.Products.TagWith("QueryingWithLike")
                .Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));
                WriteLine("Command:"+prods.ToQueryString());
                foreach (Product item in prods)
                {
                    WriteLine("{0} has {1} units in stock. Discontinued? {2}",
                    item.ProductName, item.Stock, item.Discontinued);
                }
            }
        }
        static void QueryingWithLikeLINQ()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());
                // var prodsAllQuery = from p in db.Products
                //                     select p;
                // foreach (Product item in prodsAllQuery)
                // {
                //     WriteLine("{0} has {1} units in stock. Discontinued? {2}",
                //     item.ProductName, item.Stock, item.Discontinued);
                // }
                Write("Enter part of a product name: ");
                string input = ReadLine();
                // Using Contains, not SQL Like
                var prodsQuery = from p in db.Products
                            where p.ProductName.ToUpper().Contains($"{input.ToUpper()}")
                            select p;
                var prods = prodsQuery.TagWith("QueryingWithLikeLINQ");
                WriteLine("Command:"+prodsQuery.ToQueryString());
                foreach (Product item in prodsQuery)
                {
                    WriteLine("{0} has {1} units in stock. Discontinued? {2}",
                    item.ProductName, item.Stock, item.Discontinued);
                }
            }
        }

        static void ListProducts(){

            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());
                var query = (from p in db.Products
                            orderby p.Cost descending
                            select p).ToList();
                // JSON serialization
                var jsonString =  JsonSerializer.Serialize(query);
                string fileName = "ProductList.json";
                File.WriteAllText(fileName, jsonString);
                var jsonStringRead = File.ReadAllText(fileName);
                var productListFromJson = JsonSerializer.Deserialize<List<Product>>(jsonStringRead);

                var xs = new XmlSerializer(typeof(List<Product>));
                TextWriter writer = new StreamWriter("ProductList.xml");
                xs.Serialize(writer, query);

                var xml = new XElement("products",
                    from p in query
                    select new XElement("product",
                        new XAttribute("id", p.ProductID),
                        new XElement("name", p.ProductName),
                        new XElement("cost", p.Cost)
                    )
                );
                File.WriteAllText("ProductList_LINQ.xml", xml.ToString());

                // xml serialization
                WriteLine("===========================================");
                WriteLine("List Products");
                WriteLine("===========================================");
                foreach(var item in query){
                    WriteLine("  {0:000} {1,-35} {2,8:$#,##0.00} {3,5} {4}",
                    item.ProductID, item.ProductName, item.Cost,
                    item.Stock, item.Discontinued);
                }
                WriteLine();
                WriteLine("===========================================");
                WriteLine("List Products(Json)");
                WriteLine("===========================================");
                foreach(var item in productListFromJson){
                    WriteLine("  {0:000} {1,-35} {2,8:$#,##0.00} {3,5} {4}",
                    item.ProductID, item.ProductName, item.Cost,
                    item.Stock, item.Discontinued);
                }
                WriteLine();
            }
        }
        static bool AddPrice(decimal priceLowerLimit, decimal priceIncr)
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());
                using (IDbContextTransaction t = db.Database.BeginTransaction())
                {
                    WriteLine("===========================================");
                    WriteLine("AddPrice");
                    WriteLine("===========================================");
                    WriteLine("Transaction isolation level: {0}", t.GetDbTransaction().IsolationLevel);

                    var query = from p in db.Products
                                where p.Cost > priceLowerLimit
                                select p;
                    int num = 0;
                    foreach(var p in query){
                        num++;
                        p.Cost += priceIncr;
                    }
                    // save tracked change to database
                    int affected = db.SaveChanges();
                    t.Commit();
                    return (affected == num);
                }
            }
        }
        static bool AddProduct(int categoryID, string productName, decimal? price)
        {
            using (var db = new Northwind())
            {

                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());
                using (IDbContextTransaction t = db.Database.BeginTransaction())
                {
                    WriteLine("===========================================");
                    WriteLine("AddProduct");
                    WriteLine("===========================================");
                    WriteLine("Transaction isolation level: {0}", t.GetDbTransaction().IsolationLevel);

                    var newProduct = new Product
                    {
                        CategoryID = categoryID,
                        ProductName = productName,
                        Cost = price
                    };
                    // mark product as added in change tracking
                    db.Products.Add(newProduct);
                    // save tracked change to database
                    int affected = db.SaveChanges();
                    t.Commit();
                    return (affected == 1);
                }
            }
        }

        static int DeleteProducts(string name)
        {
            using (var db = new Northwind())
            {
                // IEnumerable<Product> products = db.Products.Where(
                // p => p.ProductName.StartsWith(name));
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());
                using (IDbContextTransaction t = db.Database.BeginTransaction())
                {
                    var products = from p in db.Products
                                where p.ProductName.StartsWith(name)
                                select p;
                    WriteLine("===========================================");
                    WriteLine("Products to be Deleted");
                    WriteLine("===========================================");
                    WriteLine("Transaction isolation level: {0}", t.GetDbTransaction().IsolationLevel);

                    foreach(var item in products){
                        WriteLine("{0:000} {1,-35} {2,8:$#,##0.00} {3,5} {4}",
                        item.ProductID, item.ProductName, item.Cost,
                        item.Stock, item.Discontinued);
                    }
                    db.Products.RemoveRange(products);
                    int affected = db.SaveChanges();
                    t.Commit();
                    return affected;
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
            // QueryingCategories();
            // QueryingCategoriesQE();
            // FilteredIncludes();
            // FilteredIncludesQE();
            // FilteredIncludesQE2();
            // FilteredIncludesQE3();
            FilteredIncludesQE5();
            // QueryingProducts();
            // QueryingWithLike();
            // QueryingWithLikeLINQ();
            // -----------------------------------------------
            // Test SaveChange
            // -----------------------------------------------
            // if (AddProduct(6, "Bob's Burgers", 1000M))
            // {
            //     WriteLine ("Add product successful.");
            // }
            // if (AddProduct(6, "Bob's Meat", 5000M))
            // {
            //     WriteLine ("Add product successful.");
            // }
            // if(AddPrice(499, 10)){
            //     WriteLine("Updated successfully");
            // }
            ListProducts();
            // int deletedNum = DeleteProducts("Bob");
            // WriteLine("Remove {0} product(s)", deletedNum);

        }
    }
}
