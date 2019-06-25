using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Json;
using System.Timers;


namespace Program
{
    internal class Program
    {
        
        public static void Main(string[] args)
        {
          Sensor sensor;
          
          sensor = new Sensor("MoreTestSensor");
          sensor.RegisterCurrentMeasure(6.26);
          
          sensor.SendDataToServer();


        }

    }
}
