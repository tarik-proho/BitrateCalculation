using Serilog;

namespace BitrateCalculation.Extensions
{
    public static class SerilogContainerConfiguration
    {
        public static IHostBuilder AddSerilog(this IHostBuilder hostBuilder)
        {

            hostBuilder.UseSerilog((hostContext, services, configuration) =>
            {
                configuration.ReadFrom.Configuration(hostContext.Configuration)
                    .Enrich.FromLogContext()
                    .WriteTo.Console();
            });

            return hostBuilder;
        }
    }
}
