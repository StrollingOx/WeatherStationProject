using System.Runtime.Serialization;

namespace Program
{
   [DataContract] 
    public class HumiditySensor: Sensor, IHumidity
    {
        [DataMember(Name = "Humidity")]
        private double _humidity = 0.0d;

        public double GetHumidity()
        {
            return _humidity;
        }
    }
}