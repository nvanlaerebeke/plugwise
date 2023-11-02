using System;
using System.Threading;
using LanguageExt.Common;
using PlugwiseControl.Message;
using PlugwiseControl.Message.Requests;
using PlugwiseControl.Message.Responses;

namespace PlugwiseControl;

internal class RequestManager : IRequestManager
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
        
        Send<StickStatusResponse>(new InitializeRequest());
    }

    public Result<T> Send<T>(Message.Request request) where T : Response, new()
    {
        lock (_requestLock)
        {
            //Send a request to the plugwise stick
            _currentRequest = new Request(new T());
            _connection.Send(request);
            _wait.Reset();

            //Wait until response has been received
            if (_wait.WaitOne(2000)) {
                return _currentRequest.GetResponse<T>();
            }
            
            _receiving = string.Empty;
            _currentRequest = null;
            return new Result<T>(new TimeoutException());
        }
    }

    private void Received(string data)
    {
        //Skip '?' messages
        _receiving += data.Replace("?", string.Empty);
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
                if (IsAckMessage(stripped, out var ackMessage))
                {
                    if (ackMessage.Status != Status.Success)
                    {
                        _currentRequest.Status = ackMessage.Status;
                        _wait.Set();
                        _receiving = string.Empty;
                        break;
                    }
                }
                else
                {
                    _currentRequest.AddData(stripped);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _currentRequest.Status = Status.Failed;
                _wait.Set();
            }

            if (_currentRequest.IsComplete())
            {
                _currentRequest.Status = Status.Success;
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

    private static bool IsAckMessage(string message, out ResultResponse ackMessage)
    {
        if (message.StartsWith("0000"))
        {
            ackMessage = new ResultResponse(message);
            return true;
        }

        ackMessage = null;
        return false;
    }
}
