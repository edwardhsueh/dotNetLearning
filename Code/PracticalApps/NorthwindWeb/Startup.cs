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
using static System.Console;
using Microsoft.AspNetCore.Routing;

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
            // Adds middleware that defines a point in the pipeline where routing decisions are made 
            // and must be combined with a call to UseEndpoints where the processing is then executed. 
            // This means that for our code, any URL paths that match / or /index or /suppliers will be 
            // mapped to Razor Pages and a match on /hello will be mapped to the anonymous delegate. 
            // Any other URL paths will be passed on to the next delegate for matching, for example, static files. This is why, although it looks like the mapping for Razor Pages and /hello happen after static files in the pipeline, they actually take priority because the call to UseRouting happens before UseStaticFiles.
            app.UseRouting();
            app.Use(async (HttpContext context, Func<Task> next) =>
            {
                var rep = context.GetEndpoint() as RouteEndpoint;
                if (rep != null)
                {
                    WriteLine($"Endpoint name: {rep.DisplayName}");
                    WriteLine($"Endpoint route pattern: {rep.RoutePattern.RawText}");
                }
                if (context.Request.Path == "/bonjour")
                {
                    // in the case of a match on URL path, this becomes a terminating
                    // delegate that returns so does not call the next delegate
                    await context.Response.WriteAsync("Bonjour Monde!");
                    return; 
                }
                // we could modify the request before calling the next delegate
                await next();
                // we could modify the response after calling the next delegate
            });            
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
