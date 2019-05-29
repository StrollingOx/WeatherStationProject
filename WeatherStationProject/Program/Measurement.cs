using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    public class Measurement
    {
        private readonly Dictionary<string, double> _openWith = new Dictionary<string, double>();

        public Measurement()
        {
        }

        public void AddRecord(Sensor sensor)
        {
            String key = sensor.Name;
            double value;
            if (sensor is TemperatureSensor)
            {
                value = ((TemperatureSensor) sensor).GetTemperature();
            }
            else if (sensor is HumiditySensor)
            {
                value = ((HumiditySensor) sensor).GetHumidity();
            }
            else if (sensor is PressureSensor)
            {
                value = ((PressureSensor) sensor).GetPressure();
            }
            else
            {
                throw new UnmeasurableSensor();
            }

            _openWith.Add(key, value);
        }
        
        public void AddRecord(Sensor sensor, double value)
        {
            _openWith.Add(sensor.Name, value);
        }

        public double GetRecord(string record)
        {
            return _openWith.TryGetValue(record, out var value) ? value : -1.0;
        }
    }

    public class UnmeasurableSensor : Exception
    {
        public UnmeasurableSensor()
        {
            Console.WriteLine("Sensor do not have anything to measure!");
        }
        public UnmeasurableSensor(string message) : base(message) { }
    }
}