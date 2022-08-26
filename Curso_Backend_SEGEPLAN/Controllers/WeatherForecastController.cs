using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso_Backend_SEGEPLAN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
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

        [HttpGet(Name = "GetWeatherForecast")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("endpoint2")]
        [AllowAnonymous]
        public ActionResult<int[]> GetInt()
        {
            int[] listaEnteros = new int[]
            {
                1, 
                2, 
                3,
                4,
                5
            };

            //return Ok(listaEnteros.Where(x => x < 3).ToList());

            int cantidadItemsBorrar = 0;

            for (int i = 0; i < listaEnteros.Length; i++)
            {
                if (listaEnteros[i] > 2)
                {
                    listaEnteros[i] = 0;
                    cantidadItemsBorrar++;
                }
            }

            int[] nuevoArray = new int[listaEnteros.Length - cantidadItemsBorrar];

            for (int i = 0; i < nuevoArray.Length; i++)
            {
                nuevoArray[i] = listaEnteros[i];
            }

            return Ok(nuevoArray.ToList());
        }
    }
}