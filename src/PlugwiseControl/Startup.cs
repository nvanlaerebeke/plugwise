using System;
using Microsoft.Extensions.DependencyInjection;
using PlugwiseControl.Cache;
using PlugwiseControl.Calibration;

namespace PlugwiseControl; 

public class Startup {
    public void Start(IServiceCollection serviceCollection, string serialPort) {
        var requestManager = new RequestManager(serialPort);
        var cache = new UsageCache(requestManager, new Calibrator(requestManager));
        serviceCollection.AddSingleton<IPlugControl>(
            new PlugControl(requestManager, cache)
        );
        Console.WriteLine($"Cache duration for power usage is {UsageCache.CacheDuration} seconds");
        requestManager.Open();
    }
}
