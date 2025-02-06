using Backend.Domain;
using Backend.Infrastructure.Utils;
using Microsoft.Extensions.Options;

namespace Backend.Infrastructure.API
{
    
    /// <summary>
    /// Converts a Location to coordinates, by querying the Api Ninja Geocoding Api.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="httpClient"></param>
    public class LocationToCoordinatesConverter(IOptions<Settings> settings, HttpClient httpClient)
    {
        private readonly string _baseUrl = settings.Value.CoordinatsApiUrl;
        private readonly string _apiKey = settings.Value.CoordinatesApiKey;


        /// <summary>
        /// Queries the API.
        /// NOTE: ONLY LOCATIONS IN SWITZERLAND ARE SUPPORTED
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Coordinates Object with corresponding data from the Location</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Coordinates> GetCoordinatesForLocation(string location)
        {
            var url = $"{_baseUrl}city={location}&country=Switzerland";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("X-Api-Key", _apiKey);

            var response = await httpClient.SendAsync(request);

            var locations = await response.Content.ReadFromJsonAsync<List<LocationResponseDto>>();
            var firstLocation = locations?.FirstOrDefault();

            if (firstLocation == null)
            {
                throw new Exception("Location not found");
            }

            return new Coordinates(firstLocation.Latitude, firstLocation.Longitude);
        }

        private record LocationResponseDto
        {
            public string Name { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
        }
    }
}