namespace Orleans.TelemetryConsumers.Prometheus
{
    public class PrometheusTelemetryConsumerOptions
    {
        [Redact]
        public int MerticServerPort { get; set; }
    }
}
