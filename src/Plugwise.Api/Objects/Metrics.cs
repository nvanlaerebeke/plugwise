namespace Plugwise.Api.Objects;

public class Metrics {
    public Metrics(List<PlugMetric> metrics) {
        Plugs = metrics;
    }

    public List<PlugMetric> Plugs { get; }
}
