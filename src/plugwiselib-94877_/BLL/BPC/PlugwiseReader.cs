﻿
using System;

using PlugwiseLib.BLL.BC;
using System.Collections.Generic;
using PlugwiseLib.UTIL;

namespace plugwiseLib.BLL.BPC
{
    /*public enum PlugwiseResponseCodes
    {
        power = 1,
        calibration = 2,
        powerinfo =3,
        history =4
        
    } */
    
     
    class PlugwiseReader
    {

        /// <summary>
        /// Constructor for the Plugwise Reader class. This class reads all the received plugwise messages and returns the appropriate messages
        /// </summary>
        public PlugwiseReader()
        {
        }

        /// <summary>
        /// This method reads the serial data and performs a conversion to a message object
        /// </summary>
        /// <param name="serialData">A string array containing the data received by the serial port</param>
        /// <returns>A list of PlugwiseMessages that represent the data that was received by the serial port</returns>
        public List<PlugwiseMessage> Read(string[] serialData)
        {
            List<PlugwiseMessage> output = new List<PlugwiseMessage>();
           
                    foreach(string str in serialData)
                    {
                        Console.WriteLine("Serialdata: " + str);
                        PlugwiseMessage msg = new PlugwiseMessage();
                        if (str.Length > 40)
                        {
                            string mac = "";
                            string command = str.Substring(4, 4);
                            
                            msg.ResponseCode = command;
                            switch (command)
                            {
                                //power message
                                case "0024":

                                    mac = str.Substring(12, 16);
                                    string state = str.Substring(44, 2);
                                    string lastlog = str.Substring(32, 8);
                                    msg.Owner = mac;
                                    msg.Message = state+"|"+lastlog;
                                    msg.Type = Convert.ToInt16(PlugwiseActions.Status);
                                    output.Add(msg);

                                    break;
                                    //calibration response
                                case "0027":
                                    mac = str.Substring(12, 16);
                                    msg.Owner = mac;
                                    string gaina = str.Substring(24, 8);
                                    string gainb = str.Substring(32, 8);
                                    string offTot = str.Substring(40, 8);
                                    string offRuis = str.Substring(48, 8);
                                                                       
                                    msg.Message = ""+gaina+"|"+gainb+"|"+offTot+"|"+offRuis;
                                    msg.Type = Convert.ToInt16(PlugwiseActions.Calibration);
                                    output.Add(msg);
                                    break;
                                //current power usage
                                case "0013":
                                    mac = str.Substring(12, 16);
                                    msg.Owner = mac;
                                    float eightSec = ConversionClass.HexStringToUInt16(str.Substring(24, 4));
                                    float oneSec = ConversionClass.HexStringToUInt16(str.Substring(28, 4));
                                    msg.Message = "" + eightSec+ "|"+oneSec;
                                    msg.Type = Convert.ToInt16(PlugwiseActions.powerinfo);
                                    output.Add(msg);
                                    break;
                                //power usage history
                                case "0049":
                                    mac = str.Substring(12, 16);
                                    msg.Owner = mac;
                                    double h1 = ConversionClass.HexStringToUInt32(str.Substring(24, 8));
                                    double v1 = ConversionClass.HexStringToUInt32(str.Substring(32, 8));
                                    double h2 = ConversionClass.HexStringToUInt32(str.Substring(40, 8));
                                    double v2 = ConversionClass.HexStringToUInt32(str.Substring(48, 8));
                                    double h3 = ConversionClass.HexStringToUInt32(str.Substring(56, 8));
                                    double v3 = ConversionClass.HexStringToUInt32(str.Substring(64, 8));
                                    double h4 = ConversionClass.HexStringToUInt32(str.Substring(72, 8));
                                    double v4 = ConversionClass.HexStringToUInt32(str.Substring(80, 8)); 
                                    int logAddres = MessageHelper.ConvertPlugwiseLogHexToInt(int.Parse(str.Substring(88, 8),System.Globalization.NumberStyles.HexNumber));
                                    msg.Message = "" + h1 + "|" + v1 + "|" + h2 + "|" + v2 + "|" + h3 + "|" + v3 + "|" + h4 + "|" + v4+"|"+logAddres;
                                    msg.Type = Convert.ToInt16(PlugwiseActions.history);
                                    output.Add(msg);
                                    break;
                            }
                        }
                    }
                   
          

            return output;
        }
    }
}