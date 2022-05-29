using System.Text.RegularExpressions;

namespace PlugwiseLib.BLL.BC;

public class PlugwisePowerUsageMessage
{
    public PlugwisePowerUsageMessage(PlugwiseMessage msg)
    {
        Mac = msg.Owner;
        Type = PlugwiseActions.powerinfo;
        var values = Regex.Split(msg.Message, "\\|");
        EightSec = float.Parse(values[0]);
        OneSec = float.Parse(values[1]);
    }

    public string Mac { get; set; }

    public PlugwiseActions Type { get; set; }

    public float EightSec { get; set; }

    public float OneSec { get; set; }
}