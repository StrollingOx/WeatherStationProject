using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Program
{
    [DataContract]
    [KnownType(typeof(TemperatureSensor))]
    [KnownType(typeof(PressureSensor))]
    [KnownType(typeof(HumiditySensor))]


    public class WeatherStation
    {
        private string _stationName;
        public WeatherStation(String name)
        {
            _stationName = name;
        }
        
        public void OnMeasurementRegistered(object o, Measurement e)
        {
            Console.WriteLine(_stationName +" - warning! An event occured.");
            Console.WriteLine("Object " + (o as Sensor)?.Name + " just registered new measure.");
            for (var i = 0; i < e.Counter; i++)
            {
                var key = (o as Sensor)?.Name + "_measure_" + (i + 1);
                var value = e.GetRecord((o as Sensor)?.Name + "_measure_" + (i + 1));
                
                Console.WriteLine( key + ": " + value +"\n");
            }
        }
    }
}