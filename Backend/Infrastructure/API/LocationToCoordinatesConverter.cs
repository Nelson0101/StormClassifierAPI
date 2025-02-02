using System;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.Domain;
using Backend.Infrastructure.Utils;
using Microsoft.Extensions.Options;

namespace Backend.Infrastructure.API
{
    public class LocationToCoordinatesConverter
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public LocationToCoordinatesConverter(IOptions<Settings> settings, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = settings.Value.CoordinatsApiUrl;
            _apiKey = settings.Value.CoordinatesApiKey;
        }
        

        public async Task<Coordinates> GetCoordinatesForLocation(string location)
        {
            //https://api.api-ninjas.com/v1/geocoding?city=Affoltern am Albis&country=Switzerland
            // 
            var url = $"{_baseUrl}city={location}&country=Switzerland";
            Console.WriteLine(url);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("X-Api-Key", _apiKey);

            var response = await _httpClient.SendAsync(request);

            var locations = await response.Content.ReadFromJsonAsync<List<LocationResponse>>();
            var firstLocation = locations?.FirstOrDefault();

            if (firstLocation == null)
            {
                throw new Exception("Location not found");
            }

            return new Coordinates(firstLocation.Latitude, firstLocation.Longitude);
        }

        private class LocationResponse
        {
            public string Name { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
        }
    }
}