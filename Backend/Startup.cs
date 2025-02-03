using Backend.Application;
using Backend.Infrastructure.API;
using Backend.Infrastructure.Classifier;
using Backend.Infrastructure.Utils;

namespace Backend
{
    public class Startup(IConfiguration configuration)
    {
        private IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Configure<Settings>(Configuration);
            serviceCollection.AddHttpClient<WeatherApiCallerByCoordinates>();
            serviceCollection.AddHttpClient<LocationToCoordinatesConverter>();
            serviceCollection.AddScoped<WeatherApiCallerByLocation>();
            serviceCollection.AddScoped<Classifier>();
            serviceCollection.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}