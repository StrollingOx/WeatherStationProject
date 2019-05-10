namespace Program
{
    public class PressureSensor: Sensor, IPressure
    {
        private double _pressure = 0.0d;
        
        public double getPressure()
        {
            return _pressure;
        }
    }
}