using Backend.Application;
using Backend.Domain;
using Backend.Infrastructure.Classifier;
using Backend.Presentation.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Presentation
{
    //dependency injection 
    [ApiController]
    [Route("api/[controller]/")]
    public class DamageController(WeatherApiCallerByLocation weatherApiCallerByLocation, Classifier classifier, DateChecker dateChecker, DtoFactory dtoFactory) : ControllerBase
    {
        [HttpGet("{location}/{date}/{mainProcess}")]
        public async Task<IActionResult> GetDamage([FromRoute] string location, [FromRoute] DateTime date, [FromRoute] string mainProcess)
        {
            MainProcess mainProcessEnum;
            WeatherData? weatherData;
            if (!dateChecker.DateIsFromPreviousDay(date))
            {
                return BadRequest(dtoFactory.InvalidDateDto());
            }
            try
            {
                mainProcessEnum = GetMainProcess(mainProcess);
            }
            catch (ArgumentException)
            {
                return BadRequest(dtoFactory.InvalidMainProcessDto());
            }

            try
            {
                 weatherData = await weatherApiCallerByLocation.GetWeatherData(location, date);
            }
            catch (Exception e)
            {
                return BadRequest(dtoFactory.InvalidLocationDto());
            }
            
            var classifierData = new ClassifierData(false, weatherData.Daily, mainProcessEnum, date);
            var classification = classifier.Classify(classifierData);
            return Ok(dtoFactory.ClassificationDto(classification));
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