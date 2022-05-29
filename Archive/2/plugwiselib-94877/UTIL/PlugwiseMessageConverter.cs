using PlugwiseLib.BLL.BC;

namespace PlugwiseLib.UTIL;

public class PlugwiseMessageConverter
{
    public static PlugwisePowerUsageMessage ConvertToPowerUsage(PlugwiseMessage msg)
    {
        PlugwisePowerUsageMessage output = null;
        if (msg.Type == Convert.ToInt32(PlugwiseActions.Status))
        {
            output = new PlugwisePowerUsageMessage(msg);
        }

        return output;
    }

    public static PlugwiseCalibrationMessage ConvertToCalibrationMessage(PlugwiseMessage msg)
    {
        PlugwiseCalibrationMessage output = null;
        if (msg.Type == Convert.ToInt32(PlugwiseActions.Calibration))
        {
            output = new PlugwiseCalibrationMessage(msg);
        }

        return output;
    }

    public static PlugwiseStatusMessage ConvertToStatusMessage(PlugwiseMessage msg)
    {
        PlugwiseStatusMessage output = null;
        if (msg.Type == Convert.ToInt32(PlugwiseActions.Status))
        {
            output = new PlugwiseStatusMessage(msg);
        }

        return output;
    }
}