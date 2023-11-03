namespace Plugwise.Config;

internal class Settings : ISettings {
    public IEnumerable<string> MacAddresses => new List<string>() {
        "000D6F0001A5A3B6",
        "000D6F00004B9EA7",
        "000D6F00004B992C",
        "000D6F00004BC20A",
        "000D6F00004BF588",
        "000D6F00004BA1C6",
        "000D6F000076B9B3",
        "000D6F0000D31AB8"
    };
    
    public string SerialPort {
        get {
            var serialPort = Environment.GetEnvironmentVariable("PLUGWISE_SERIAL_PORT");
            if (serialPort is null) {
                throw new ArgumentException("Environment variable PLUGWISE_SERIAL_PORT not set", SerialPort);
            }

            if (!File.Exists(serialPort)) {
                throw new Exception($"Serial port {serialPort} not found");
            }

            return serialPort;
        }
    }
}
