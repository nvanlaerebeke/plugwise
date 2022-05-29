using System;
using System.Threading;

var quit = new ManualResetEvent(false);
Console.CancelKeyPress += ((_, _) => quit.Set());

Console.WriteLine("Hello, World!");

var _quit = new ManualResetEvent(false);
    
Console.WriteLine("Staring Application...");
Thread.CurrentThread.Name = "MainThread";
        
//Start Controller Thread
new Thread(delegate()
{
    Controller.Controller.Start();
}).Start();

Console.CancelKeyPress += (_, _) =>
{
    _quit.Set();
};

_quit.WaitOne();
    
Console.WriteLine("Bye World!");