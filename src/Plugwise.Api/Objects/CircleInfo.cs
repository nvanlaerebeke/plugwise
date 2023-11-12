using System.Text.Json.Serialization;
using PlugwiseControl.Message;

namespace Plugwise.Api.Objects;

public class CircleInfo {
    
    /// <summary>
    /// Mag address for the plug
    /// </summary>
    [JsonPropertyName("mac")]
    public string Mac { get; set; }
    
    /// <summary>
    /// True if updating the state is allowed. (On/Off)
    /// </summary>
    [JsonPropertyName("allowStateUpdate")]
    public bool AllowStateUpdate { get; set; }

    /// <summary>
    /// Human readable name for the plug
    /// </summary>
    [JsonPropertyName("name")] 
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Address of the Circle+ plug
    /// This is the gateway to all the other plugs
    /// The plugwise stick communicates only with this Circle
    /// </summary>
    [JsonPropertyName("circle_plus_mac")] 
    public string CirclePlusMac { get; set; } = string.Empty;
    
    /// <summary>
    /// Current date set for the plug
    /// </summary>
    [JsonPropertyName("datetime")] 
    public DateTime Date { get; set; }

    /// <summary>
    /// Current year set for the plug
    /// </summary>
    [JsonPropertyName("year")] 
    public int Year { get; set; }

    /// <summary>
    /// Current month set for the plug
    /// </summary>
    [JsonPropertyName("month")] 
    public int Month { get; set; }

    [JsonPropertyName("min")] 
    public string Min { get; set; } = string.Empty;

    [JsonPropertyName("current_log")] 
    public string CurrentLog { get; set; } = string.Empty;

    /// <summary>
    /// State of the plug, is it on or off
    /// </summary>
    [JsonPropertyName("switch_state")] 
    [JsonConverter(typeof(JsonStringEnumConverter))]
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
    
    /// <summary>
    /// Current usage in Wh
    /// </summary>
    [JsonPropertyName("usage")]
    public double Usage { get; set; }

    /// <summary>
    /// Unit used for the usage
    /// </summary>
    public string Unit { get; } = "Wh";
}
