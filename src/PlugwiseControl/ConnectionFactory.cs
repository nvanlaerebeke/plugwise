namespace PlugwiseControl;

internal class ConnectionFactory
{
    public Connection Get(string serialPort)
    {
        return new Connection(serialPort);
    }
}