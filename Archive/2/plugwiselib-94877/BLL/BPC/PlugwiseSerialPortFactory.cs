namespace PlugwiseLib.BLL.BPC;

public enum PlugwiseSerialPortRequest
{
    on,
    off,
    status,
    calibration,
    powerinfo,
    history
}

public class PlugwiseSerialPortFactory
{
    /// <summary>
    ///     This factory returns the strings that will be sent to the serial port when a certain command needs the be activated
    /// </summary>
    /// <param name="req">The type of request that has to be sent to the plugs</param>
    /// <param name="mac">The mac address of the receiver</param>
    /// <returns></returns>
    public static string Create(PlugwiseSerialPortRequest req, string mac)
    {
        var output = "";
        switch (req)
        {
            case PlugwiseSerialPortRequest.on:
                output = GetOnMessage(mac);
                break;
            case PlugwiseSerialPortRequest.off:
                output = getOffMessage(mac);
                break;
            case PlugwiseSerialPortRequest.status:
                output = getStatusMessage(mac);
                break;
            case PlugwiseSerialPortRequest.calibration:
                output = getCalibrationMessage(mac);
                break;
            case PlugwiseSerialPortRequest.powerinfo:
                output = getPowerinfoMessage(mac);
                break;
        }

        return output;
    }

    public static string Create(PlugwiseSerialPortRequest req, string logId, string mac)
    {
        var output = "";
        switch (req)
        {
            case PlugwiseSerialPortRequest.history:

                output = getHistoryMessage(mac, logId);
                break;
        }

        return output;
    }

    private static string getPowerinfoMessage(string mac)
    {
        var output = "";
        var crc = new Crc16Ccitt(InitialCrcValue.Zeros);
        var crcValue = crc.ComputeChecksumString("0012" + mac);
        output = GetStart() + "0012" + mac + crcValue + GetEnd();
        return output;
    }

    private static string GetOnMessage(string mac)
    {
        var output = "";
        var crc = new Crc16Ccitt(InitialCrcValue.Zeros);
        var crcValue = crc.ComputeChecksumString("0017" + mac + "01");
        output = GetStart() + "0017" + mac + "01" + crcValue + GetEnd();

        return output;
    }

    private static string getOffMessage(string mac)
    {
        var output = "";
        var crc = new Crc16Ccitt(InitialCrcValue.Zeros);
        var crcValue = crc.ComputeChecksumString("0017" + mac + "00");
        output = GetStart() + "0017" + mac + "00" + crcValue + GetEnd();
        return output;
    }

    private static string getStatusMessage(string mac)
    {
        var output = "";
        var crc = new Crc16Ccitt(InitialCrcValue.Zeros);
        var crcValue = crc.ComputeChecksumString("0023" + mac);
        output = GetStart() + "0023" + mac + crcValue + GetEnd();
        return output;
    }

    private static string getCalibrationMessage(string mac)
    {
        var output = "";
        var crc = new Crc16Ccitt(InitialCrcValue.Zeros);
        var crcValue = crc.ComputeChecksumString("0026" + mac);
        output = GetStart() + "0026" + mac + crcValue + GetEnd();
        return output;
    }

    private static string getHistoryMessage(string mac, string logId)
    {
        var output = "";
        var crc = new Crc16Ccitt(InitialCrcValue.Zeros);
        var crcValue = crc.ComputeChecksumString("0048" + mac + logId);
        output = GetStart() + "0048" + mac + logId + crcValue + GetEnd();
        return output;
    }


    public static string GetStart()
    {
        return "" + (char) 5 + (char) 5 + (char) 3 + (char) 3;
    }

    public static string GetEnd()
    {
        return "" + (char) 13 + (char) 10;
    }
}