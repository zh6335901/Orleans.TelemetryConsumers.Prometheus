using Microsoft.Extensions.DependencyInjection;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.TelemetryConsumers.Prometheus;

namespace Orleans.Hosting
{
    public static class ConfigurationExtensions
    {
        public static ISiloBuilder AddPrometheusTelemetryConsumer(this ISiloBuilder hostBuilder, int port = 1234)
        {
            return hostBuilder.ConfigureServices(services => ConfigureServices(services, port));
        }

        private static void ConfigureServices(IServiceCollection services, int port)
        {
            var bootStrapper = new PrometheusMetricServerBootStrapper();
            bootStrapper.Start(port);

            services.AddSingleton(bootStrapper);
            services.Configure<TelemetryOptions>(options => options.AddConsumer<PrometheusTelemetryConsumer>());
        }
    }
}
