using System;

namespace PlugwiseControl.Message.Requests;

internal class SetDateTimeRequest : PlugRequest
{
    private readonly long _unixDStamp;

    public SetDateTimeRequest(string mac, long unixDStamp) : base("0016", mac)
    {
        _unixDStamp = unixDStamp;
    }

    public override string ToString()
    {
        var dateTimeOffSet = DateTimeOffset.FromUnixTimeSeconds(_unixDStamp);
        var dateTime = dateTimeOffSet.DateTime;

        var year = dateTime.Year;
        var month = dateTime.Month;
        var minutes = Convert.ToInt32((dateTime - new DateTime(year, month, 01)).TotalMinutes);

        var dateString = int.Parse(year.ToString().Substring(2, 2)).ToString("X").PadLeft(2, '0') +
                         month.ToString("X").PadLeft(2, '0') +
                         minutes.ToString("X").PadLeft(4, '0')
            ;

        return GetStart() + Code + Mac + dateString + new Crc16Ccitt(InitialCrcValue.Zeros).ComputeChecksumString(Code + Mac + dateString) + GetEnd();
    }
}