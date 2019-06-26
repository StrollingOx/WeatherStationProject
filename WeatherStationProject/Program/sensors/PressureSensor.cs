using System;
using System.Runtime.Serialization;

namespace Program
{
    [DataContract]
    public class PressureSensor: Sensor, IPressure
    {
        [DataMember(Name="Pressure")]
        private double _pressure = 0.0d;
        
        public double GetPressure()
        {
            return _pressure;
        }

        public void SetPressure(double value)
        {
            _pressure = value;
        }

        public override string ToString()
        {
            return Name + ": " + GetPressure() + "Pa";
        }
    }
}