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
        
        public override string ToString()
        {
            return Name + ": " + GetPressure() + "Pa";
        }
    }
}