namespace PlugwiseLib.UTIL;

public class MessageHelper
{
    public static string ConvertIntToPlugwiseLogHex(int logId)
    {
        var output = "";
        var newlogId = 278528 + 32 * logId;
        output = newlogId.ToString("X8");
        return output;
    }

    public static int ConvertPlugwiseLogHexToInt(int logId)
    {
        var output = 0;
        var newlogId = (logId - 278528) / 32;
        output = newlogId;
        return output;
    }

    /// <summary>
    ///     This method checks if a plugwise hour number deliveres a correct date.
    /// </summary>
    /// <param name="hours">THe number of hours to add to the start date</param>
    /// <returns>true when the date is correct, false when the date is not correct</returns>
    public static bool TryCalculatePlugwiseDate(long hours)
    {
        var output = true;
        try
        {
            var date = new DateTime(2007, 6, 1, 0, 0, 0, DateTimeKind.Utc);
            date = date.AddHours(hours);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            output = false;
        }

        return output;
    }

    public static DateTime CalculatePlugwiseDate(long hours)
    {
        try
        {
            var output = new DateTime(2007, 6, 1, 0, 0, 0, DateTimeKind.Utc);
            output = output.AddHours(hours);
            return output;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new Exception("");
        }
    }
}