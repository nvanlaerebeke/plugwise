using System.IO.Ports;
using System.Text.RegularExpressions;
using PlugwiseLib.BLL.BC;
using plugwiseLib.BLL.BPC;
using PlugwiseLib.BLL.BPC;
using PlugwiseLib.UTIL;

namespace PlugwiseLib;

public class plugwiseControl
{
    public delegate void PlugwiseDataReceivedEvent(object sender, EventArgs e, List<PlugwiseMessage> data);

    private PlugwiseActions currentAction;
    private readonly SerialPort port;

    private readonly PlugwiseReader reader;

    /// <summary>
    ///     Constructor for the PlugwiseControl Control class
    /// </summary>
    /// <param name="serialPort">The serial port name that the plugwise stick takes</param>
    public plugwiseControl(string serialPort)
    {
        try
        {
            port = new SerialPort(serialPort);
            port.DataReceived += port_DataReceived;

            port.BaudRate = 115200;
            currentAction = PlugwiseActions.None;
            reader = new PlugwiseReader();
        }
        catch (Exception e)
        {
            throw new Exception("Could not connect to plug.");
        }
    }

    public event PlugwiseDataReceivedEvent DataReceived;

    /// <summary>
    ///     This is the method that sends a command to the plugwise plugs.
    /// </summary>
    /// <param name="mac">The mac adress of the plug that needs to perform the action</param>
    /// <param name="action">The action that has to be performed</param>
    public void Action(string mac, PlugwiseActions action)
    {
        try
        {
            var message = "";
            switch (action)
            {
                case PlugwiseActions.On:
                    currentAction = PlugwiseActions.On;
                    message = PlugwiseSerialPortFactory.Create(PlugwiseSerialPortRequest.on, mac);


                    break;
                case PlugwiseActions.Off:
                    currentAction = PlugwiseActions.Off;
                    message = PlugwiseSerialPortFactory.Create(PlugwiseSerialPortRequest.off, mac);


                    break;
                case PlugwiseActions.Status:

                    currentAction = PlugwiseActions.Status;
                    message = PlugwiseSerialPortFactory.Create(PlugwiseSerialPortRequest.status, mac);

                    break;
                case PlugwiseActions.Calibration:
                    currentAction = PlugwiseActions.Calibration;
                    message = PlugwiseSerialPortFactory.Create(PlugwiseSerialPortRequest.calibration, mac);


                    break;
                case PlugwiseActions.powerinfo:
                    currentAction = PlugwiseActions.powerinfo;
                    message = PlugwiseSerialPortFactory.Create(PlugwiseSerialPortRequest.powerinfo, mac);

                    break;
                case PlugwiseActions.history:
                    message = "";
                    break;
            }

            if (message.Length > 0)
            {
                Console.WriteLine($"Writing: {message}");
                port.WriteLine(message);
                Thread.Sleep(10);
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    ///     This is the method that sends a command to the plugwise plugs that retrieves the history power information
    /// </summary>
    /// <param name="mac">The mac adress of the plug that needs to perform the action</param>
    /// <param name="logId">The id of the history message that has to be retrieved</param>
    /// <param name="action">The action that has to be performed this MUST be history</param>
    public void Action(string mac, int logId, PlugwiseActions action)
    {
        var message = "";
        switch (action)
        {
            case PlugwiseActions.history:
                currentAction = PlugwiseActions.history;
                message = PlugwiseSerialPortFactory.Create(PlugwiseSerialPortRequest.history, MessageHelper.ConvertIntToPlugwiseLogHex(logId), mac);
                break;
        }

        if (message.Length > 0)
        {
            port.WriteLine(message);
            Thread.Sleep(10);
        }
    }

    private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        // Event for receiving data

        var txt = port.ReadExisting();
        var msg = reader.Read(Regex.Split(txt, "\r\n"));
        if (msg.Count > 0 && DataReceived != null)
        {
            DataReceived(sender, new EventArgs(), msg);
        }
    }

    /// <summary>
    ///     This method Opens the connection to the serial port
    /// </summary>
    public void Open()
    {
        try
        {
            if (!port.IsOpen)
            {
                port.Open();
            }
        }
        catch (IOException ex)
        {
            throw ex;
        }
    }

    /// <summary>
    ///     This method closes the connection to the serial port
    /// </summary>
    public void Close()
    {
        try
        {
            if (port.IsOpen)
            {
                port.Close();
            }

            Thread.Sleep(5);
        }
        catch (IOException ex)
        {
            throw ex;
        }
    }
}