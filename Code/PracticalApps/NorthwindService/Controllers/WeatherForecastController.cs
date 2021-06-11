using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
namespace NorthwindService.Controllers
{
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<WeatherForecast>))]
        [ProducesResponseType(404)]
        public IActionResult Get() // original method
        {
            IEnumerable<WeatherForecast> result;
            result = Get(5);
            if(result == null){
                return NotFound();
            }
            else{
                return Ok(result);
            }
        }

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
