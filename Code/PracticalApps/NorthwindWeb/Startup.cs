using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore; 
using Packt.Shared;

namespace NorthwindWeb
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // add statements to add Razor Pages and its related services like model binding, authorization, anti-forgery, views, and tag helpers
            services.AddRazorPages();    
            // register the Northwind database context class to use SQLite as its database provider and 
            // specify its database connection string, as shown in the following code:
            string databasePath = Path.Combine("..", "Northwind.db");
            services.AddDbContext<Northwind>(options =>    options.UseSqlite($"Data Source={databasePath}"));            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // HTTP Strict Transport Security (HSTS) is an opt-in security enhancement. 
                // If a website specifies it and a browser supports it, then it forces all 
                // communication over HTTPS and prevents the visitor from using untrusted or invalid certificates.
                app.UseHsts();
            }            

            app.UseRouting();
            // redirect HTTP requests to HTTPS
            app.UseHttpsRedirection();
            // The call to UseDefaultFiles must be before the call to UseStaticFiles, or it won't work!
            app.UseDefaultFiles(); // index.html, default.html, and so on
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapGet("/hello", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
