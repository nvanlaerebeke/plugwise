using System.Text.RegularExpressions;
using PlugwiseLib.UTIL;

namespace PlugwiseLib.BLL.BC;

public class PlugwiseStatusMessage : PlugwiseMessage
{
    public PlugwiseStatusMessage(PlugwiseMessage msg)
    {
        Mac = msg.Owner;
        Type = PlugwiseActions.Status;
        var values = Regex.Split(msg.Message, "\\|");
        On = Convert.ToBoolean(Convert.ToInt32(values[0]));
        LastLog = MessageHelper.ConvertPlugwiseLogHexToInt((int) ConversionClass.HexStringToUInt32(values[1]));
    }

    public bool On { get; set; }

    public int LastLog { get; set; }

    public string Mac { get; set; }

    public PlugwiseActions Type { get; set; }
}