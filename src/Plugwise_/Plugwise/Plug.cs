using System;
using System.Collections.Generic;
using PlugwiseLib;
using PlugwiseLib.BLL.BC;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace Controller
{
    class Plug
    {
        private string _strMac;
        private bool? _blnOn;

        public Plug(string pMac)
        {
            Console.WriteLine("Added Plug " + pMac);
            _strMac = pMac;
        }

        public string Mac
        {
            get { return _strMac; }
        }

        public bool? On
        {
            get
            {
                if (_blnOn == null)
                {
                    Console.WriteLine("Current plug state is unknown");
                    Console.WriteLine("Getting current plug status...");
                    if (Platform.IsLinux)
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        string strFileName = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + Path.DirectorySeparatorChar + "plugwise_util";
                        Console.WriteLine(strFileName);
                        startInfo.FileName = strFileName;
                        startInfo.Arguments = "-m " + Mac + " -q relay_state";
                        startInfo.UseShellExecute = false;
                        startInfo.RedirectStandardOutput = true;
                        startInfo.CreateNoWindow = true;
                        Process objProc = new Process();
                        objProc.StartInfo = startInfo;

                        objProc.Start();
                        while (!objProc.StandardOutput.EndOfStream)
                        {
                            _blnOn = (objProc.StandardOutput.ReadLine() == "1") ? true : false;
                            break;
                        }
                    }
                    else
                    {
                        PlugRequest objRequest = new PlugRequest();
                        PlugwiseStatusMessage objResponse = objRequest.Request<PlugwiseStatusMessage>(this, PlugwiseActions.Status);
                        _blnOn = objResponse.On;
                    }
                    Console.WriteLine("Current Status is " + ((_blnOn == true) ? "ON" : "OFF"));
                }
                return _blnOn;
            }
            set
            {
                Console.WriteLine("Setting Plug " + Mac + " to " + value.ToString());
                _blnOn = (value == null) ? false : value;

                plugwiseControl objConnection = PlugConnectionManager.GetConnection();
                Console.WriteLine("Setting plug status of " + Mac + " to " + ((_blnOn == true) ? "ON" : "OFF"));
                if (value == false)
                {
                    objConnection.Action(Mac, PlugwiseActions.Off);
                }
                else
                {
                    objConnection.Action(Mac, PlugwiseActions.On);
                }
                Console.WriteLine("Status Changed");
            }
        }

        void objConnection_DataReceived(object sender, EventArgs e, List<PlugwiseMessage> data)
        {
            throw new NotImplementedException();
        }
    }
}
