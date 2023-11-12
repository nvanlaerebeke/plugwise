using System.Text.Json.Serialization;
using CrazyMobile.Config;
using PlugwiseControl.Message;
using PlugwiseControl.Message.Responses;

namespace Plugwise.Api.Objects; 

public class PlugInfo : PlugMetric {
    public PlugInfo(Plug plug, Usage usage, CircleInfoResponse circleInfo) : base(plug, usage) {
        AllowUpdateState = plug.PowerControl;
        SwitchState = circleInfo.State;
        Mac = plug.Mac;
    }
    
    [JsonPropertyName("mac")]
    public string Mac { get; }
    
    [JsonPropertyName("switchState")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SwitchState SwitchState { get; }
    
    [JsonPropertyName("allowUpdateState")]
    public bool AllowUpdateState { get; }
}
