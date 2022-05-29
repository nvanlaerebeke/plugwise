using System;
using System.Threading;
using PlugwiseControl.Message.Responses;

namespace PlugwiseControl;

internal class RequestManager
{
    private readonly Connection _connection;
    private readonly object _requestLock = new();
    private readonly ManualResetEvent _wait = new(false);
    private Request _currentRequest;

    private string _receiving = string.Empty;

    public RequestManager(string serialPort)
    {
        _connection = new ConnectionFactory().Get(serialPort);
        _connection.OnDataReceived(Received);
        _connection.Open();
    }

    public T Send<T>(Message.Request request) where T : Response, new()
    {
        lock (_requestLock)
        {
            //Send a request to the plugwise stick
            _currentRequest = new Request(new T());
            _connection.Send(request);

            //Wait until response has been received
            if (!_wait.WaitOne(5000))
            {
                _receiving = string.Empty;
                _currentRequest = null;
                throw new Exception("Timeout waiting for stick");
            }

            _wait.Reset();

            //return the response
            return _currentRequest.GetResponse<T>();
        }
    }

    private void Received(string data)
    {
        //Skip '?' messages
        _receiving += data.Replace("?", string.Empty);
        Console.WriteLine($"Current buffer: {_receiving}");
        while (true)
        {
            //Waiting for the end of the message
            if (!_receiving.Contains("\r\n"))
            {
                break;
            }
            
            var index = _receiving.IndexOf("\r\n", StringComparison.Ordinal);
            var message = _receiving[..index]; //First Message
            _receiving = _receiving[(index + 2)..]; //"the rest"

            try
            {
                if (message.Length <= 4)
                {
                    continue;
                }

                var stripped = !message.StartsWith('#') ? message[4..] : message;
                Console.WriteLine("Message: " + stripped);
                _currentRequest.AddData(stripped);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _wait.Set();
            }

            if (_currentRequest.IsComplete())
            {
                _wait.Set();
                _receiving = string.Empty;
                break;
            }

            //there is still data to read
            if (_receiving != string.Empty)
            {
                continue;
            }

            //nothing to do, buffer is empty
            break;
        }
    }
}