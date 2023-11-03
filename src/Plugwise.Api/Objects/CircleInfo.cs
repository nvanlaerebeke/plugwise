using System.Text.Json.Serialization;
using PlugwiseControl.Message;

namespace Plugwise.Api.Objects;

public class CircleInfo {
    [JsonPropertyName("circle_plus_mac")] 
    public string CirclePlusMac { get; set; } = string.Empty;

    [JsonPropertyName("datetime")] 
    public DateTime Date { get; set; }

    [JsonPropertyName("year")] 
    public int Year { get; set; }

    [JsonPropertyName("month")] 
    public int Month { get; set; }

    [JsonPropertyName("min")] 
    public string Min { get; set; } = string.Empty;

    [JsonPropertyName("current_log")] 
    public string CurrentLog { get; set; } = string.Empty;

    [JsonPropertyName("switch_state")] 
    public SwitchState State { get; set; }

    [JsonPropertyName("hertz")] 
    public string Hertz { get; set; } = string.Empty;

    [JsonPropertyName("hw1")] 
    public string HW1 { get; set; } = string.Empty;

    [JsonPropertyName("hw2")] 
    public string HW2 { get; set; } = string.Empty;

    [JsonPropertyName("hw3")] 
    public string HW3 { get; set; } = string.Empty;

    [JsonPropertyName("firmware")] 
    public string Firmware { get; set; } = string.Empty;

    [JsonPropertyName("type")] 
    public string Type { get; set; } = string.Empty;
}
