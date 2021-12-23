# Orleans.TelemetryConsumers.Prometheus
Prometheus implementation of Orleans Telemetry API.

# Installation
    dotnet add package Orleans.TelemetryConsumers.Prometheus

# Usage
Register in code before initializing the Orleans Silo:
    
    var builder = new SiloHostBuilder()
        .AddPrometheusTelemetryConsumer()

    // Or
    // Use self prometheus metrics server
    var builder = new SiloHostBuilder()
        // publishing the metrics on http://*:1234/metrics
        .AddPrometheusTelemetryConsumerWithSelfServer(port: 1234)
        
Then access http://host:port/metrics, You can see the content like below:

    ...
    orleans_metrics{category="Storage",name="Storage.Read.Total.Current"} 1
    orleans_metrics{category="Catalog",name="Catalog.Activation.Destroyed.Current"} 0
    orleans_metrics{category="Networking",name="Networking.Sockets.Silo.Closed.Current"} 0
    orleans_metrics{category="Messaging",name="Messaging.Processing.Dispatcher.Processed.Ok.Direction.Response.Current"} 1
    orleans_metrics{category="App",name="App.Requests.Total.Requests.Current"} 222
    orleans_metrics{category="Messaging",name="Messaging.Sent.LocalMessages.Current"} 434
    ...

# Grafana dashboard

