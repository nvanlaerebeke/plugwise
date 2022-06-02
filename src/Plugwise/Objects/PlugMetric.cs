using System.Text.Json.Serialization;

namespace Plugwise.Objects;

public class PlugMetric
{
    public PlugMetric(string mac, Usage usage)
    {
        Mac = mac;
        Usage = usage.PowerConsumption;
        Unit = usage.Unit;
    }

    [JsonPropertyName("mac")]
    public string Mac { get; set; }

    [JsonPropertyName("usage")]
    public double Usage { get; set; }

    [JsonPropertyName("unit")]
    public string Unit { get; set; }
}