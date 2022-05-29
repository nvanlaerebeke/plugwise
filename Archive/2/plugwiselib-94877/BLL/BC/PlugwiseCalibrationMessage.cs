using System.Text.RegularExpressions;
using PlugwiseLib.UTIL;

namespace PlugwiseLib.BLL.BC;

public class PlugwiseCalibrationMessage
{
    public PlugwiseCalibrationMessage(PlugwiseMessage msg)
    {
        Mac = msg.Owner;
        Type = PlugwiseActions.Calibration;
        var values = Regex.Split(msg.Message, "\\|");
        GainA = ConversionClass.HexStringToFloat(values[0]);

        GainB = ConversionClass.HexStringToFloat(values[1]);

        OffRuis = ConversionClass.HexStringToFloat(values[2]);
        OffTot = ConversionClass.HexStringToFloat(values[3]);
    }

    public string Mac { get; set; }

    public PlugwiseActions Type { get; set; }

    public float GainA { get; set; }

    public float GainB { get; set; }

    public float OffTot { get; set; }

    public float OffRuis { get; set; }
}