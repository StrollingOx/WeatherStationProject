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
        [DataMember(Name="List of sensor")]
        private readonly List<Sensor> _sensors = new List<Sensor>();
        
        
        public WeatherStation(){}
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
            List<Sensor> tSensors = new List<Sensor>();
            List<Sensor> hSensors = new List<Sensor>();
            List<Sensor> pSensors = new List<Sensor>();

            foreach (var sensor in _sensors)
            {
                if (sensor is TemperatureSensor)
                {
                    tSensors.Add(sensor);
                }

                if (sensor is HumiditySensor)
                {
                    hSensors.Add(sensor);
                }

                if (sensor is PressureSensor)
                {
                    pSensors.Add(sensor);
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
    }
}