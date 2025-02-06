using Backend.Domain;
using Backend.Infrastructure.Classifier;
using Backend.Presentation.Dtos;

namespace Backend.Application
{
    public class DamageService(
        WeatherApiCallerByLocation weatherApiCaller,
        Classifier classifier,
        DateChecker dateChecker,
        DtoFactory dtoFactory)
    {
        
        /// <summary>
        /// Processes a Request coming from the Controller. This is the main logic of the application.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="date"></param>
        /// <param name="mainProcess"></param>
        /// <returns>A Dto. In case of successs, a ClassificationDto is returned. In case of failure, the corresponding
        /// ErrorDto is returned.</returns>
        public async Task<IDto> ProcessDamageRequest(string location, DateTime date, string mainProcess)
        {
            if (!dateChecker.DateIsFromPreviousDay(date))
                return dtoFactory.InvalidDateDto();

            var mainProcessEnum = ParseMainProcess(mainProcess);
            if (mainProcessEnum == null)
                return dtoFactory.InvalidDateDto();

            var weatherData = await FetchWeatherData(location, date);
            if (weatherData == null)
                return dtoFactory.InvalidLocationDto();

            var classifierData = new ClassifierData(false, weatherData.Daily, mainProcessEnum.Value, date);
            var classification = classifier.Classify(classifierData);
            return dtoFactory.ClassificationDto(classification);
        }

        private MainProcess? ParseMainProcess(string mainProcess)
        {
            if (Enum.TryParse(mainProcess, out MainProcess result) && Enum.IsDefined(typeof(MainProcess), result))
                return result;

            return null;
        }

        private async Task<WeatherData?> FetchWeatherData(string location, DateTime date)
        {
            try
            {
                return await weatherApiCaller.GetWeatherData(location, date);
            }
            catch
            {
                return null;
            }
        }
    }
}