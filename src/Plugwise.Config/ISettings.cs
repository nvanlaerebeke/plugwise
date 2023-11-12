using CrazyMobile.Config;

namespace Plugwise.Config;

public interface ISettings {
    List<Plug> Plugs { get; }
    string SerialPort { get; }
}
