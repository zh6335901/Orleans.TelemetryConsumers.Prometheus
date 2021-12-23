using Microsoft.Extensions.DependencyInjection;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.TelemetryConsumers.Prometheus;
using System;

namespace Orleans.Hosting
{
    public static class ConfigurationExtensions
    {
        public static ISiloBuilder AddPrometheusTelemetryConsumer(
            this ISiloBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(
                services => ConfigureServices(services, false, null));
        }

        public static ISiloBuilder AddPrometheusTelemetryConsumerWithSelfServer(
            this ISiloBuilder hostBuilder,
            int port = 1234)
        {
            return hostBuilder.ConfigureServices(
                services => ConfigureServices(services, true, port));
        }

        public static ISiloHostBuilder AddPrometheusTelemetryConsumer(
            this ISiloHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(
                services => ConfigureServices(services, false, null));
        }

        public static ISiloHostBuilder AddPrometheusTelemetryConsumerWithSelfServer(
            this ISiloHostBuilder hostBuilder, 
            int port = 1234)
        {
            return hostBuilder.ConfigureServices(
                services => ConfigureServices(services, true, port));
        }

        public static IClientBuilder AddPrometheusTelemetryConsumer(
            this IClientBuilder clientBuilder)
        {
            return clientBuilder.ConfigureServices(
                (_, services) => ConfigureServices(services, false, null));
        }

        public static IClientBuilder AddPrometheusTelemetryConsumerWithSelfServer(
            this IClientBuilder clientBuilder, 
            int port = 1234)
        {
            return clientBuilder.ConfigureServices(
                (_, services) => ConfigureServices(services, true, port));
        }

        private static void ConfigureServices(
            IServiceCollection services, 
            bool selfServer, 
            int? selfServerPort)
        {
            if (selfServer)
            {
                var _ = selfServerPort ?? throw new ArgumentNullException(nameof(selfServerPort));

                var bootStrapper = new PrometheusMetricServerBootStrapper();
                bootStrapper.Start(selfServerPort.Value);

                services.AddSingleton(bootStrapper);
            }

            services.Configure<TelemetryOptions>(
                options => options.AddConsumer<PrometheusTelemetryConsumer>());
        }
    }
}
