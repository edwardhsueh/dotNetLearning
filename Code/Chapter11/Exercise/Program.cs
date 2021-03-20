using System;
using Edward.Shared;
using Microsoft.EntityFrameworkCore;
using System.IO;
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
                // Console.Write("Create Database SQL:"+sql);
                dbContext.Database.EnsureCreated();
            }
        }

    }
}
