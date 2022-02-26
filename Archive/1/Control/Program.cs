using System;
using System.Threading;

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
                Console.WriteLine("Starting Controller");
                System.Threading.Thread.CurrentThread.Name = "Controller";
                Controller.Start();
            }).Start();

            for (; ; ) { }
        }
    }
}
