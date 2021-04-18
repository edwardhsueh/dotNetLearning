using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Packt.Shared;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text.Json;

namespace Packt.Shared
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
            using (var dbContext = new Northwind()){
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
        }
    }
}
