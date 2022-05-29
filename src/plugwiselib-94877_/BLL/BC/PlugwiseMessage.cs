using System;
using System.Collections.Generic;

using System.Text;

namespace PlugwiseLib.BLL.BC
{
    public class PlugwiseMessage
    {
        private string _owner;

        public string Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }
        private int _type;

        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _strResponseCode;
        public string ResponseCode
        {
            get { return _strResponseCode; }
            set { _strResponseCode = value; }
        }
    }
}
