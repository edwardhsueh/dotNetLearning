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
            using (var dbContext = new EdwardDb()){
                var query =
                    from blog in dbContext.Blogs
                    join post in dbContext.Posts on blog.BlogId equals post.MainBlogId
                    select new
                    {
                        id = blog.BlogId,
                        id2 = post.PostId,
                    };
                Console.WriteLine("query:{0}", query.ToQueryString());
                var exeq = query.ToList();
                foreach (var q in exeq){
                    Console.WriteLine(q.ToString());
                }
                // foreach( var q in query){
                //     //  Console.WriteLine("bid:{0}, pid:{1}, pblogid:{2}, pTitle:{3}", q.blogId, q.pId, q.pBlogId, q.pTitle);
                //     Console.WriteLine();
                // }

            }
        }

    }
}
