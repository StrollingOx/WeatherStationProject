using System;
using System.Collections.Generic;
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
        public List<ITemperature> searchSensorsByTemperature(double temp)
        {
            return _sensors.OfType<ITemperature>().Where(_sensors => _sensors.GetTemperature() >= temp).ToList();
        }
        public IEnumerable<Type> searchSensors(Type type, Func<Type, bool> condition, double temperature)
        {
            var selectedItems = _sensors.OfType<Type>().Where(condition);
            return selectedItems;
        }
    }
}