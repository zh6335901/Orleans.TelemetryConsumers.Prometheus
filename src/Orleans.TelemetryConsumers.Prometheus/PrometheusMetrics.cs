using Prometheus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orleans.TelemetryConsumers.Prometheus
{
    internal static class PrometheusMetrics
    {
        internal readonly static Counter ExceptionCounter = Metrics
            .CreateCounter("orleans_exception_counter", "Count of orleans exception");

        internal readonly static Counter EventCounter = Metrics
            .CreateCounter("orleans_events_counter", "Count of orleans events",
                new CounterConfiguration { LabelNames = new[] { "name" } });

        internal readonly static Gauge MetricGauge = Metrics
            .CreateGauge("orleans_metrics", "Orleans metrics", 
                new GaugeConfiguration { LabelNames = new[] { "category", "name" } });

        internal readonly static Histogram MetricDuration = Metrics
            .CreateHistogram("orleans_metrics_duration", "Orleans metrics duration",
            new HistogramConfiguration { LabelNames = new[] { "category", "name" } });

        internal readonly static Histogram RequestDuration = Metrics
            .CreateHistogram("orleans_requests_duration", "Orleans requests duration",
            new HistogramConfiguration { LabelNames = new[] { "name", "code", "success" } });

        internal readonly static Histogram DependencyDuration = Metrics
            .CreateHistogram("orleans_dependency_duration", "Orleans requests duration",
            new HistogramConfiguration { LabelNames = new[] { "dependency_name", "command_name", "success" } });
    }
}
