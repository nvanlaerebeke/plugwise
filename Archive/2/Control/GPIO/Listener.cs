using System;
using Raspberry.IO.GeneralPurpose;
using System.Threading;

namespace Controller
{
    public delegate void StatusChanged();
    public class Listener
    {
        private Thread _objListenThread;
        private ConnectorPin _objPin;
        private SwitchType _objType;

        public event StatusChanged statusChanged;

        public Listener(ConnectorPin pPin, SwitchType pType)
        {
            _objPin = pPin;
            _objType = pType;
        }

        public void Start()
        {

            Console.WriteLine("Starting Listener for " + _objPin.ToString());
            var procPin = _objPin.ToProcessor();
            var driver = GPIOConnectionManager.GetConnection();
            driver.Allocate(procPin, PinDirection.Input);
            new Thread(delegate()
            {
                Thread.CurrentThread.Name = "Switch listener for " + _objPin.ToString();
                while (true)
                {
                    try
                    {
                        var isHigh = driver.Read(procPin);
                        driver.Wait(procPin, !isHigh, 1 * 60 * 60 * 24 * 7);
                        isHigh = driver.Read(procPin);
                        Console.WriteLine(isHigh);
                        if ((_objType == SwitchType.HighOnly && isHigh) || _objType == SwitchType.HighAndLow)
                        {
                            Console.WriteLine("Change Detected on " + _objPin.ToString());
                            if (statusChanged != null)
                            {
                                statusChanged();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }).Start();
        }

        public void Stop()
        {
            _objListenThread.Abort();
            _objListenThread = null;
        }
    }
}
