namespace Plugwise.Config;

public interface ISettings {
    IEnumerable<string> MacAddresses { get; }
    string SerialPort { get; }
}
