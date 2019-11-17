using Prometheus;
using System;

namespace Orleans.TelemetryConsumers.Prometheus
{
    internal class PrometheusMetricServerBootStrapper : IDisposable
    {
        private static MetricServer _metricServer;

        public void Start(int port)
        {
            lock (_metricServer)
            {
                if (_metricServer == null)
                {
                    _metricServer = new MetricServer(port);
                    _metricServer.Start();
                }
            }
        }

        public void Dispose()
        {
            _metricServer?.Stop();
        }
    }
}
