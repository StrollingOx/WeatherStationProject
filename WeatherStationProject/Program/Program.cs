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
          Sensor sensor2;
          
          sensor = new Sensor("MoreTestSensor");
          sensor.RegisterCurrentMeasure(6.26);
          
          sensor2 = new TemperatureSensor();
          sensor2.Name = "TestSensorTemprr";
          (sensor2 as TemperatureSensor).SetTemperature(90.5);
          
          sensor.SendDataToServer((int) SensorType.Sensor);
          sensor2.SendDataToServer((int) SensorType.TemperatureSensor);


        }

    }
}
