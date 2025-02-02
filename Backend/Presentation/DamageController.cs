using Backend.Application;
using Backend.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Presentation
{
}

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class DamageController(WeatherApiCallerByLocation weatherApiCallerByLocation) : ControllerBase
    {
        [HttpGet("{location}/{date}/{mainProcess}")]
        public async Task<IActionResult> GetDamage([FromRoute] string location, [FromRoute] DateTime date, [FromRoute] string mainProcess)
        {
            var weatherData = await weatherApiCallerByLocation.GetWeatherData(location, date);

            return Ok(weatherData);
        }
        
    }
}