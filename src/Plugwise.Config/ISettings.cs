namespace Plugwise.Config;

public interface ISettings {
    List<string> MacAddresses { get; }
    string SerialPort { get; }
}
