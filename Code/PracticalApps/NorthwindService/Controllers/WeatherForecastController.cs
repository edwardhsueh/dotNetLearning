using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NorthwindService.Controllers
{
// The [Route] attribute registers the weatherforecast relative URL for clients to use to make HTTP requests that will be handled by this controller. For example, an HTTP request for http://localhost:5001/weatherforecast/ would be handled by this controller. Some developers like to prefix the controller name with api/, which is a convention to differentiate between MVC and Web API in mixed projects. If you use [controller] as shown, it uses the characters before Controller in the class name, in this case, WeatherForecast, or you can simply enter a different name without the square brackets, for example, [Route("api/forecast")].
// The [ApiController] attribute was introduced with ASP.NET Core 2.1 and it enables REST-specific behavior for controllers, like automatic HTTP 400 responses for invalid models, as you will see later in this chapter.    
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        // public IEnumerable<WeatherForecast> Get()
        // {
        //     var rng = new Random();
        //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //     {
        //         Date = DateTime.Now.AddDays(index),
        //         TemperatureC = rng.Next(-20, 55),
        //         Summary = Summaries[rng.Next(Summaries.Length)]
        //     })
        //     .ToArray();
        // }
        // GET /weatherforecast
        [HttpGet]
        public IEnumerable<WeatherForecast> Get() // original method
        {
            return Get(5); // five day forecast
        }
        // GET /weatherforecast/7
        [HttpGet("{days:int}")]
        public IEnumerable<WeatherForecast> Get(int days) // new method
        {
            var rng = new Random();
            return Enumerable.Range(1, days).Select(index =>
                new WeatherForecast
                {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}
