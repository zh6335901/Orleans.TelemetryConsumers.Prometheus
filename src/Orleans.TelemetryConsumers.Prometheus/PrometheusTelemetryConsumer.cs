using Orleans.Runtime;
using System;
using System.Collections.Generic;

namespace Orleans.TelemetryConsumers.Prometheus
{
    public class PrometheusTelemetryConsumer : IEventTelemetryConsumer, IExceptionTelemetryConsumer,
        IDependencyTelemetryConsumer, IMetricTelemetryConsumer, IRequestTelemetryConsumer
    {
        public void DecrementMetric(string name)
        {
            PrometheusMetrics
                .MetricGauge
                .WithLabels(GetCategoryFromMetricName(name), name)
                .Dec();
        }

        public void DecrementMetric(string name, double value)
        {
            PrometheusMetrics
                .MetricGauge
                .WithLabels(GetCategoryFromMetricName(name), name)
                .Dec(value);
        }

        public void IncrementMetric(string name)
        {
            PrometheusMetrics
                .MetricGauge
                .WithLabels(GetCategoryFromMetricName(name), name)
                .Inc();
        }

        public void IncrementMetric(string name, double value)
        {
            PrometheusMetrics
                .MetricGauge
                .WithLabels(GetCategoryFromMetricName(name), name)
                .Inc(value);
        }

        public void TrackDependency(string dependencyName, string commandName, DateTimeOffset startTime, TimeSpan duration, bool success)
        {
            PrometheusMetrics
                .DependencyDuration
                .WithLabels(dependencyName, commandName, success.ToString())
                .Observe(duration.TotalSeconds);
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            PrometheusMetrics.EventCounter.Inc();
            AddMetrics(metrics);
        }

        public void TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            PrometheusMetrics.ExceptionCounter.Inc();
            AddMetrics(metrics);
        }

        public void TrackMetric(string name, double value, IDictionary<string, string> properties = null)
        {
            PrometheusMetrics
                .MetricGauge
                .WithLabels(GetCategoryFromMetricName(name), name)
                .Set(value);
        }

        public void TrackMetric(string name, TimeSpan value, IDictionary<string, string> properties = null)
        {
            PrometheusMetrics
                .MetricDuration
                .WithLabels(GetCategoryFromMetricName(name), name)
                .Observe(value.TotalSeconds);
        }

        public void TrackRequest(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success)
        {
            PrometheusMetrics
                .RequestDuration
                .WithLabels(name, responseCode, success.ToString())
                .Observe(duration.TotalSeconds);
        }

        public void Flush()
        {
        }

        public void Close()
        {
        }

        private void AddMetrics(IDictionary<string, double> metrics)
        {
            if (metrics != null)
            {
                foreach (var kv in metrics)
                {
                    PrometheusMetrics
                        .MetricGauge
                        .WithLabels(GetCategoryFromMetricName(kv.Key), kv.Key)
                        .Set(kv.Value);
                }
            }
        }

        private string GetCategoryFromMetricName(string name)
        {
            return name.Substring(0, name.IndexOf('.'));
        }
    }
}
