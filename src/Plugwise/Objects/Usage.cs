using System.Text.Json.Serialization;

namespace Plugwise.Objects;

public class Usage
{
    public Usage(double usage, string unit)
    {
        PowerConsumption = usage;
        Unit = unit;
    }

    [JsonPropertyName("power_consumption")]
    public double PowerConsumption { get; }

    [JsonPropertyName("unit")]
    public string Unit { get; }
}