using Backend.Application;
using Backend.Domain;
using Backend.Infrastructure.Classifier;
using Backend.Presentation.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Presentation
{
    //dependency injection done by Framework
    [ApiController]
    [Route("api/[controller]/")]
    public class DamageController(WeatherApiCallerByLocation weatherApiCallerByLocation, Classifier classifier) : ControllerBase
    {
        [HttpGet("{location}/{date}/{mainProcess}")]
        public async Task<IActionResult> GetDamage([FromRoute] string location, [FromRoute] DateTime date, [FromRoute] string mainProcess)
        {
            MainProcess mainProcessEnum;
            WeatherDataDto? weatherData;
            try
            {
                mainProcessEnum = GetMainProcess(mainProcess);
            }
            catch (ArgumentException e)
            {
                return BadRequest(new ErrorDto(e.Message));
            }

            try
            {
                 weatherData = await weatherApiCallerByLocation.GetWeatherData(location, date);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorDto(e.Message));
            }
            
            var classifierData = new ClassifierData(false, weatherData.Daily, mainProcessEnum, date);
            var classification = classifier.Classify(classifierData);
            return Ok(new ClassificationDto(classification));
        }

        private MainProcess GetMainProcess(string mainProcess)
        {
            if (Enum.TryParse(mainProcess, out MainProcess result) && Enum.IsDefined(result))
            {
                return result;
            }
            throw new ArgumentException("Main Process must be a value of 1, 2 or 3");
        }
        
    }
}