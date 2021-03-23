using System;
using Edward.Shared;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;

namespace Exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            // using is important

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
                IQueryable<Blog> qblog = dbContext.Blogs
                    .Where(b => b.BlogId == 1);
                // var blogs = qblog.ToList();
                Console.WriteLine("query:{0}", qblog.ToQueryString());
                foreach( Blog blog in qblog){
                    Console.WriteLine("blog id:{0}, url:{1}", blog.BlogId, blog.Url);
                }
            }
        }

    }
}
