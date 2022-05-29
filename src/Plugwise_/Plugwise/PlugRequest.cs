using System;
using System.Collections.Generic;
using PlugwiseLib.BLL.BC;
using PlugwiseLib;
using System.Threading;

namespace Controller
{
    class PlugRequest
    {
        private EventWaitHandle blockThread;
        private Plug _objPlug;
        private PlugwiseActions _enuAction;
        private PlugwiseMessage _objResponseMessage;

        public T Request<T>(Plug pPlug, PlugwiseActions pAction) {
            _objPlug = pPlug;
            _enuAction = pAction;

            plugwiseControl objConnection = PlugConnectionManager.GetConnection();
            objConnection.DataReceived += new plugwiseControl.PlugwiseDataReceivedEvent(objConnection_DataReceived);

            objConnection.Action(pPlug.Mac, pAction);
            
            blockThread = new AutoResetEvent(false);
            blockThread.WaitOne();

            return (T)Activator.CreateInstance(typeof(T), new object[] { _objResponseMessage });
        }

        void objConnection_DataReceived(object sender, EventArgs e, List<PlugwiseMessage> data)
        {
            if (data[0].Owner == _objPlug.Mac && data[0].ResponseCode == PlugResponseCode.GetResponseCode(_enuAction))
            {
                _objResponseMessage = data[0];
                blockThread.Set();

            }
            Console.WriteLine("DATA RECIEVED");
        }
    }
}
