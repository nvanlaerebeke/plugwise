using System;
using System.Threading;
using System.IO.Ports;
using System.Text;
using XBee;
using XBee.Frames;

namespace Controller
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Staring Application...");
            Console.WriteLine("Running on " + ((Platform.IsLinux) ? "Linux" : "Windows"));

            Thread.CurrentThread.Name = "MainThread";
            //Start Controller Thread
            new Thread(delegate ()
            {
                Controller.Start();
            }).Start();

            for (; ; ) { }
        }
    }
}
