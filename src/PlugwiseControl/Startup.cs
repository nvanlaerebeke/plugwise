using Microsoft.Extensions.DependencyInjection;
using PlugwiseControl.Cache;
using PlugwiseControl.Calibration;

namespace PlugwiseControl; 

public class Startup {
    public void Start(IServiceCollection serviceCollection, string serialPort) {
        var requestManager = new RequestManager(serialPort);
        
        serviceCollection.AddSingleton<IPlugControl>(
            new PlugControl(requestManager, new UsageCache(requestManager, new Calibrator(requestManager)))
        );
    }
}
