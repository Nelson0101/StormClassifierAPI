using Backend.Application;
using Backend.Infrastructure.API;
using Backend.Infrastructure.Classifier;
using Backend.Infrastructure.Services;
using Backend.Infrastructure.Utils;
using Backend.Presentation.Dtos;

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
            serviceCollection.AddScoped<ZScoreNormalizer>();
            serviceCollection.AddScoped<TensorFactory>();
            serviceCollection.AddScoped<Classifier>();
            serviceCollection.AddScoped<DateChecker>();
            serviceCollection.AddScoped<DtoFactory>();
            serviceCollection.AddScoped<DamageClassificationService>();
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