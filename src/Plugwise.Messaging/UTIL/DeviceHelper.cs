using System;
using System.Collections.Generic;
using System.Text;


namespace PlugwiseLib.UTIL
{
    public static class DeviceHelper
    {

        public static double CalculatePowerUsage(float offRuis, float GainA, float GainB, float OffTot, float pulses, int timeMeasure)
        {

            //calculation is like this: 3600 * (((pow(value + offruis, 2.0) * gain_b) + ((value + offruis) * gain_a)) + offtot)
            double output = 0;
            
            double value = pulses /timeMeasure;
            

            //do: pow(value + offruis, 2.0) * gain_b
            double first = (Math.Pow(value + offRuis,2) * GainB);
            //do:((value + offruis) * gain_a)
            double second = ((value + offRuis) * GainA) ;
            //calculate the total and add  * 
            double total = (first + second) + OffTot;
            //multiply total by the timemeasure 3600 in the original formula
            output = timeMeasure * total;
            //go to watt
          
            return output;
        }

        public static double ConverPulsesToWatt(int timespan,int pulses)
        {
            double output = ConvertPulsesToKwh(timespan, pulses);
            output = output * 1000;
            return output;
        }

        public static double ConvertPulsesToKwh(int timespan, int pulses)
        {
            double output = (pulses / timespan) / 468.9385193;

            return output;
        }
    }
}
