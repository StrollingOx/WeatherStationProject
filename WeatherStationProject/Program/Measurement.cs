using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    public class Measurement
    {
        //TODO: the Measurement class is supposed to contain measure's values! Not sensors
        private readonly Dictionary<string, Sensor> _openWith = new Dictionary<string, Sensor>();

        public Measurement(){}

        public void AddRecord(string key, Sensor value)
        {
            _openWith.Add(key, value);
        }

        public Sensor GetRecord(string record)
        {
            return _openWith.TryGetValue(record, out var value) ? value : null;
        }

        
    }
}