namespace PlugwiseControl;

internal class PlugwiseReader
{
    /// <summary>
    ///     Constructor for the PlugwiseControl Reader class. This class reads all the received plugwise messages and returns the appropriate messages
    /// </summary>
    public PlugwiseReader()
    {
    }

    /// <summary>
    ///     This method reads the serial data and performs a conversion to a message object
    /// </summary>
    /// <param name="data">The string received from the serial port</param>
    /// <returns>A list of PlugwiseMessages that represent the data that was received by the serial port</returns>
    /*public Response Read(string data)
    {
        if (data.Length < 8)
        {
            return null;
        }

        Console.WriteLine($"Received: {data}");
        var msg = data[4..];
        var command = msg[..4];

        Response response = null;
        switch (command)
        {
            //Act response
            case "0000":
                response = new ResultResponse(msg);
                break;
            //switch on ack
            case "00D8":
                response = new SwitchOnAck(msg);
                break;
            
            //switch off ack
            case "00DE":
                response = new SwitchOffAck(msg);
                break;
            
            case "0011":
                response = new CircleInfoResponse(msg);
                break;
            
            //power message
            case "0024":
                response = new PowerResponse(msg);
                break;
            
            //calibration response
            case "0027":
                response = new CalibrationResponse(msg);
                break;
            
            //current power usage
            case "0013":
                response = new PowerUsageResponse(msg);
                break;
            
            //power usage history
            case "0049":
                response = new PowerUsageHistoryEntry(msg);
                break;
            default:
                return null;
        }

        if (response != null)
        {
            if (response.IsComplete())
            {
                return response;
            }

            throw new Exception("incomplete");
        }

        return null;
    }*/
}