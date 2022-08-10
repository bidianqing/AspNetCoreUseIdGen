using IdGen;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreUseIdGen.Controllers
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

        private readonly IdGenerator _idGenerator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IdGenerator idGenerator)
        {
            _logger = logger;
            _idGenerator = idGenerator;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var id = _idGenerator.CreateId();


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}