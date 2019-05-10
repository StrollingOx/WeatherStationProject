namespace Program
{
    public class HumiditySensor: Sensor, IHumidity
    {
        private double _humidity = 0.0d;

        public double getHumidity()
        {
            return _humidity;
        }
    }
}