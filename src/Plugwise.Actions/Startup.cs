using Microsoft.Extensions.DependencyInjection;
using PlugwiseControl;

namespace Plugwise.Actions;

public class Startup {
    public void Setup(IServiceCollection serviceCollection, string serialPort) {
        serviceCollection.AddSingleton<IPlugService, PlugService>();

        new PlugwiseControl.Startup().Start(serviceCollection, serialPort);
    }
}
