using System;
using Edward.Shared;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            // using is important
            string file = Path.Combine(Environment.CurrentDirectory, "db.log");
            using(var fs = File.CreateText(file)){
                fs.WriteLine("** Created on: "+ DateTime.Now.ToString());
            }
            using (var dbContext = new EdwardDb()){
                dbContext.Database.EnsureDeleted();
                var sql = dbContext.Database.GenerateCreateScript();
                string fileName = Path.Combine(Environment.CurrentDirectory, "output.sql");
                Console.Write("Write Create Database SQL to file:{0}",fileName);
                using (var fsWrite = File.CreateText(fileName)){
                    fsWrite.WriteLine(sql);
                }
                Console.Write("Create Database SQL:"+sql);
                dbContext.Database.EnsureCreated();
            }
            using (var dbContext = new EdwardDb()){
                var loggerFactory = dbContext.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());
                using (IDbContextTransaction t = dbContext.Database.BeginTransaction()){
                    Console.WriteLine("Transaction isolation level: {0}", t.GetDbTransaction().IsolationLevel);
                    IQueryable<Blog> qblog = dbContext.Blogs.TagWith("QueryPost")
                        .Where(b => b.BlogId == 1);
                    // var blogs = qblog.ToList();
                    // Console.WriteLine("query:{0}", qblog.ToQueryString());
                    foreach( Blog blog in qblog){
                        Console.WriteLine("blog id:{0}, url:{1}", blog.BlogId, blog.Url);
                    }
                    foreach( Blog blog in qblog){
                        blog.Url = "http://u.1.com";
                    }
                    int affected = dbContext.SaveChanges();
                    t.Commit();
                    Console.WriteLine("Update {0} line", affected);

                }
            }
            using (var dbContext = new EdwardDb()){
                var loggerFactory = dbContext.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());
                var query =
                    from blog in dbContext.Blogs
                    join post in dbContext.Posts on blog.BlogId equals post.MainBlogId
                    select new
                    {
                        id = blog.BlogId,
                        id2 = post.PostId,
                    };
                // Execute Command
                var exeq = query.TagWith("Inner Join").ToList();
                // Json Serializer
                var jsonString =  JsonSerializer.Serialize(exeq);
                File.WriteAllText("InnerJoin.json", jsonString);

                foreach (var q in exeq){
                    Console.WriteLine(q.ToString());
                }
                // foreach( var q in query){
                //     //  Console.WriteLine("bid:{0}, pid:{1}, pblogid:{2}, pTitle:{3}", q.blogId, q.pId, q.pBlogId, q.pTitle);
                //     Console.WriteLine();
                // }

            }
            using (var dbContext = new EdwardDb()){
                var loggerFactory = dbContext.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());
                using (IDbContextTransaction t = dbContext.Database.BeginTransaction()){
                    try{
                        Console.WriteLine("Transaction isolation level: {0}", t.GetDbTransaction().IsolationLevel);
                        var newBlog = new Blog { BlogId = 10, Url = "http://100000000000000000000000000000.com" , NameMapId=4};
                        // var newBlog = new Blog { BlogId = 10, Url = "http://1000.com" , NameMapId=4};
                        dbContext.Blogs.Add(newBlog);
                        int affected = dbContext.SaveChanges();
                        t.Commit();
                        Console.WriteLine("{0} line(s) affected", affected);
                        var query = from b in dbContext.Blogs
                                    select b;
                        foreach(var b in query){
                            Console.WriteLine("blog {0}/{1}", b.BlogId, b.Url, b.NameMapId);
                        }
                    }
                    catch(Exception ex){
                        Console.WriteLine($"{ex.GetType()} says {ex.Message}");

                    }
                }
            }
        }

    }
}
