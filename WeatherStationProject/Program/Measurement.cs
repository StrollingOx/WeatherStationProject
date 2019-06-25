using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Program
{
    [DataContract]
    public class Measurement: EventArgs
    {
        
        private readonly Dictionary<string, double> _openWith;
        [DataMember(Name="Measurements")]
        public Dictionary<string, double> OpenWith => _openWith;
        private int _counter;
        [DataMember(Name="Counter")]
        public int  Counter => _counter;

        public Measurement()
        {
            _openWith = new Dictionary<string, double>();
            _counter = 0;
        }

        public void AddRecord(Sensor sensor)
        {
            String key = sensor.Name + "_measure_" + ++_counter;
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
            _openWith.Add(sensor.Name+ "_measure_" + ++_counter, value);
        }

        public double GetRecord(string record)
        {
            return _openWith.TryGetValue(record, out var value) ? value : -1.0;
        }
        
        public double GetRecord(string record, int index)
        {
            return _openWith.TryGetValue(record+"_measure_"+index, out var value) ? value : -1.0;
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