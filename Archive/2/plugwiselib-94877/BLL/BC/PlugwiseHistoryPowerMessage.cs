using System.Text.RegularExpressions;
using PlugwiseLib.UTIL;

namespace PlugwiseLib.BLL.BC;

public class PlugwiseHistoryPowerMessage
{
    private long prevHour;

    public PlugwiseHistoryPowerMessage(PlugwiseMessage msg)
    {
        prevHour = 0;
        Messages = new List<PlugwiseHistoryMessage>();
        Mac = msg.Owner;
        var values = Regex.Split(msg.Message, "\\|");

        string[] hours = {values[0], values[2], values[4], values[6]};
        string[] pulses = {values[1], values[3], values[5], values[7]};

        for (var i = 0; i < hours.Length; i++)
        {
            AddMessage(hours[i], pulses[i]);
        }

        LogAddress = Convert.ToInt32(values[8]);

        Type = PlugwiseActions.history;
    }

    public string Mac { get; set; }

    public PlugwiseActions Type { get; set; }

    public List<PlugwiseHistoryMessage> Messages { get; set; }

    public int LogAddress { get; set; }

    private void AddMessage(string hourValue, string MeasurementValue)
    {
        var msg = new PlugwiseHistoryMessage();

        if (CheckHours(long.Parse(hourValue)) && MessageHelper.TryCalculatePlugwiseDate(long.Parse(hourValue)))
        {
            msg.Hourvalue = MessageHelper.CalculatePlugwiseDate(long.Parse(hourValue));
            msg.MeasurementValue = int.Parse(MeasurementValue) / 3600;
            prevHour = int.Parse(hourValue);
        }
        else
        {
            msg.Hourvalue = null;
            msg.MeasurementValue = -1;
        }

        Messages.Add(msg);
    }

    private bool CheckHours(long hours)
    {
        var output = false;
        if (prevHour == 0)
        {
            prevHour = hours;
            output = true;
        }
        else
        {
            if (hours - prevHour == 1)
            {
                output = true;
            }
        }

        return output;
    }
}

public class PlugwiseHistoryMessage
{
    public DateTime? Hourvalue { get; set; }

    public int MeasurementValue { get; set; }
}