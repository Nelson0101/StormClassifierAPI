using Backend.Domain;
using Backend.Infrastructure.Utils;
using Microsoft.Extensions.Options;

namespace Backend.Infrastructure.API
{
    public class LocationToCoordinatesConverter(IOptions<Settings> settings, HttpClient httpClient)
    {
        private readonly string _baseUrl = settings.Value.CoordinatsApiUrl;
        private readonly string _apiKey = settings.Value.CoordinatesApiKey;


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