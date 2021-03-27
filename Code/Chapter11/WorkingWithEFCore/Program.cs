using System;
using static System.Console;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
                // foreach (var result in query){
                //     Category cat = result.cat;
                //     Product  prod = result.prod;
                //     // var prodGroup = result.prodGroup;
                //     WriteLine($"{cat.CategoryName} has {prod.ProductName}");
                // }


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
                            join prod in db.Products on cat.CategoryID equals prod.CategoryID
                            select new {cat, prod};
                WriteLine("Inner Query String:\n\n"+query.ToQueryString());
                // client Evaluation for group
                var queryResult = query.AsEnumerable();
                var groupResult = from qr in queryResult
                                  group qr by new {qr.cat.CategoryName, qr.cat.CategoryID};
                foreach (var gr in groupResult)
                {
                    WriteLine(gr.Key+ " has " + gr.Count() + " Products");
                    // foreach(var grval in gr){
                    //     var c = grval.cat;
                    //     WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
                    // }
                }


            }
        }


        static void QueryingCategories()
        {
            using (var db = new Northwind()) // using is important
            {
                WriteLine("Categories and how many products they have:");
                // a query to get all categories and their related products
                IQueryable<Category> cats = db.Categories.Include(c => c.Products);
                WriteLine(cats.ToQueryString());
                foreach (Category c in cats)
                {
                    WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
                }
            }
        }
/// <summary>
/// Left Join using LINQ Expression
/// </summary>
        static void FilteredIncludesQE()
        {
            using (var db = new Northwind())
            {
                Write("Enter a minimum for units in stock: ");
                string unitsInStock = ReadLine();
                int stock = int.Parse(unitsInStock);
                var prodQuery = from prod in db.Products
                                where prod.Stock > stock
                                select prod;
                var query = from cat in db.Categories
                            join prod in prodQuery
                            on cat.CategoryID equals prod.CategoryID into prodGroup
                            from subProd in prodGroup.DefaultIfEmpty()
                            orderby cat.CategoryID ascending, subProd.ProductID ascending
                            select new {cat, subProd};
                WriteLine($"** ToQueryString: {query.ToQueryString()}");
                var qResult = query.ToList();
                foreach (var qr in qResult) {
                    // var prodName = (qr.subProd == null) ? "Nothing" : qr.subProd.ProductName;
                    var prodName = qr.subProd?.ProductName ?? "Nothing";
                    WriteLine(qr.cat.CategoryName + "___" + prodName);
                }
                var grQuery = from q in qResult
                              group q by q.cat.CategoryName;
                foreach (var gr in grQuery){
                    foreach(var grVal in gr){
                        WriteLine($"{gr.Key} has {grVal.cat.Products.Count} products with a minimum of {stock} units in stock.");
                        foreach (var p in grVal.cat.Products){
                                WriteLine($" {p.ProductName} has {p.Stock} units in stock.");
                        }

                    }
                }
                // WriteLine($"** grQuery: {grQuery.Count()}");
                // if(grQuery.Count() > 0){
                //     foreach (var gr in grQuery){
                //         // WriteLine($"{gr.Key} has {gr.Count()} products with a minimum of {stock} units in stock.");
                //         foreach (var gItem  in gr){
                //             var prod = gItem.subProd;
                //             WriteLine($" {prod.ProductName} has {prod.Stock} units in stock.");
                //         }
                //     }
                // }
                // else {
                //     WriteLine("No Product has stock more than {0} units", stock);
                // }
                // IQueryable<Category> cats = db.Categories
                // .Include(c => c.Products.Where(p => p.Stock >= stock));
                // WriteLine($"ToQueryString: {cats.ToQueryString()}");
                // foreach (Category c in cats)
                // {
                //     WriteLine($"{c.CategoryName} has {c.Products.Count} products with a minimum of {stock} units in stock.");
                // foreach(Product p in c.Products)
                // {
                //     WriteLine($" {p.ProductName} has {p.Stock} units in stock.");
                // }
                // }
            }
        }
/// <summary>
/// InterJoin
/// </summary>
        static void FilteredIncludesQE2()
        {
            using (var db = new Northwind())
            {
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
                            select new {cat, prod};
                WriteLine($"** ToQueryString: {query.ToQueryString()}");
                var qResult = query.ToList();
                foreach (var qr in qResult) {
                    var prodName = qr.prod?.ProductName ?? "Nothing";
                    WriteLine(qr.cat.CategoryName + "___" + prodName);
                }
                var grQuery = from q in qResult
                              group q by q.cat.CategoryName;
                if(grQuery.Count() > 0){
                    foreach (var gr in grQuery){
                        foreach(var grVal in gr){
                            WriteLine($"{gr.Key} has {grVal.cat.Products.Count} products with a minimum of {stock} units in stock.");
                            foreach (var p in grVal.cat.Products){
                                 WriteLine($" {p.ProductName} has {p.Stock} units in stock.");
                            }
                        }
                    }
                }
                else {
                    WriteLine($"No one has products with a minimum of {stock} in stock");
                }
            }
        }

/// <summary>
/// Left Join
/// </summary>
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
            // QueryingCategories();
            tryJoin();
            QueryingCategoriesQE();
            FilteredIncludes();
            FilteredIncludesQE();
            FilteredIncludesQE2();
            // QueryingProducts();
        }
    }
}
