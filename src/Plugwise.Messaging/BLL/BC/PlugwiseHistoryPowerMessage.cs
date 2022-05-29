using System;
using System.Collections.Generic;

using System.Text;
using plugwiseLib.BLL.BPC;
using System.Text.RegularExpressions;
using PlugwiseLib.UTIL;

namespace PlugwiseLib.BLL.BC
{
    public class PlugwiseHistoryPowerMessage
    {
        private string _Mac;

        public string Mac
        {
            get { return _Mac; }
            set { _Mac = value; }
        }

        private PlugwiseActions _type;

        public PlugwiseActions Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private List<PlugwiseHistoryMessage> _messages;

        public List<PlugwiseHistoryMessage> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }

        private int _LogAddress;

        public int LogAddress
        {
            get { return _LogAddress; }
            set { _LogAddress = value; }
        }

        private long prevHour;

        public PlugwiseHistoryPowerMessage(PlugwiseMessage msg)
        {
            prevHour = 0;
            Messages = new List<PlugwiseHistoryMessage>();
            Mac = msg.Owner;
            string[] values = Regex.Split(msg.Message, "\\|");

            string[] hours = { values[0], values[2], values[4], values[6] };
            string[] pulses = { values[1], values[3], values[5], values[7] };
           
            for (int i = 0; i < hours.Length; i++)
            {
                this.AddMessage(hours[i], pulses[i]);
            }
           
            this.LogAddress = Convert.ToInt32(values[8]);

            this.Type = PlugwiseActions.history;


        }

        private void AddMessage(string hourValue, string MeasurementValue)
        {
            PlugwiseHistoryMessage msg = new PlugwiseHistoryMessage();
            
            if (CheckHours(Int64.Parse(hourValue)) && MessageHelper.TryCalculatePlugwiseDate(Int64.Parse(hourValue)))
            {
                
                msg.Hourvalue = MessageHelper.CalculatePlugwiseDate(Int64.Parse(hourValue));
                msg.MeasurementValue = int.Parse(MeasurementValue) / 3600;
                prevHour = int.Parse(hourValue);
            }
            else
            {
                msg.Hourvalue = null;
                msg.MeasurementValue = -1;
            }
            Messages.Add(msg);
        }

        private bool CheckHours(long hours)
        {
            bool output = false;
            if (prevHour == 0)
            {
                prevHour = hours;
                output = true;
                
            }
            else
            {
                if ((hours - prevHour) == 1)
                {
                    output = true;
                }
            }
            return output;
        }
    }


    public class PlugwiseHistoryMessage
    {
        private DateTime? _Hourvalue;

        public DateTime? Hourvalue
        {
            get { return _Hourvalue; }
            set { _Hourvalue = value; }
        }
        private int _MeasurementValue;

        public int MeasurementValue
        {
            get { return _MeasurementValue; }
            set { _MeasurementValue = value; }
        }

        public PlugwiseHistoryMessage()
        {
        }
    }
}
