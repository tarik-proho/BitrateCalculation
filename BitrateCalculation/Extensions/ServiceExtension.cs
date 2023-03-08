using BitrateCalculation.Configuration;
using BitrateCalculation.Services;
using BitrateCalculation.Services.Interfaces;

namespace BitrateCalculation.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        public static void ConfigureDataServices(this IServiceCollection services)
        {
            services.AddScoped<IDriverService, DriverService>();
        }        
    }
}
