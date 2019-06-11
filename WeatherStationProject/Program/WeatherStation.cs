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
        
        [DataMember(Name = "List of sensor")] 
        private readonly List<Sensor> _sensors;
        

        public WeatherStation()
        {
            _sensors = new List<Sensor>();
        }
        public void AddSensor(Sensor sensor)
        {
            _sensors.Add(sensor);
        }
        public void GetAllTemperatures()
        {
            StringBuilder result = new StringBuilder("WeatcherStation -> Temperature sensors :\n");
            foreach (Sensor sensor in _sensors)
            {
                if (sensor is ITemperature)
                {
                    result.Append(sensor.Name+": "
                                      + (sensor as ITemperature).GetTemperature()+ " "
                                      + (sensor as ITemperature).GetScaleSymbol()
                                      +"\n");
                }
                
            }
            
            Console.Write(result);
        }
        
        public List<Sensor> SearchAllSensors(int sensorType, Func<Sensor , bool> condition)
        {
            //TODO: Optimize
            var tSensors = new List<Sensor>();
            var hSensors = new List<Sensor>();
            var pSensors = new List<Sensor>();

            foreach (var sensor in _sensors)
            {
                switch (sensor)
                {
                    case TemperatureSensor _:
                        tSensors.Add(sensor);
                        break;
                    case HumiditySensor _:
                        hSensors.Add(sensor);
                        break;
                    case PressureSensor _:
                        pSensors.Add(sensor);
                        break;
                }
            }
            
            switch(sensorType)
            {
                case 0: return tSensors.Where(condition).ToList();
                case 1: return hSensors.Where(condition).ToList();
                case 2: return pSensors.Where(condition).ToList();
                default: return _sensors.Where(condition).ToList();
            }
        }

        public void OnMeasurementRegistered(object o, Measurement e)
        {
            Console.WriteLine("Object " + (o as Sensor)?.Name + " just registered new measure.");
            for (var i = 0; i < e.Counter; i++)
            {
                var key = (o as Sensor)?.Name + "_measure_" + (i + 1);
                var value = e.GetRecord((o as Sensor)?.Name + "_measure_" + (i + 1));
                
                Console.WriteLine( key + ": " + value);
            }
        }
    }
}