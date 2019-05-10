using System;

namespace Program
{
    public class TemperatureSensor: Sensor, ITemperature
    {
        private string _scale = "";
        private double _temperature = 0.0d;

        public TemperatureSensor()
        {
            _scale = "Celsius";
        }

        public TemperatureSensor(int scale)
        {
            setScale(scale);
        }
        
        public void setScale(int scale)
        {
            switch (scale)
            {
                case 0:
                    if (_scale.Equals("Fahrenheit"))
                    {
                        convertToCelsius();
                    }
                    _scale = "Celsius";
                    break;
                case 1: 
                    if (_scale.Equals("Celsius"))
                    {
                        convertToFahrenheit();
                    }
                    _scale = "Fahrenheit";
                    break;
                default: throw new WrongScaleException();
                    
            }
        
        }

        private void convertToCelsius()
        {
            _temperature = (_temperature - 32) * 5.0 / 9.0;
        }
        
        private void convertToFahrenheit()
        {
            _temperature = (_temperature * 9.0 / 5.0) + 32;
        }

        public string getScaleName()
        {
            return _scale;
        }

        public double getTemperature()
        {
            return _temperature;
        }
    }

    public class WrongScaleException : Exception
    {
        public WrongScaleException()
        {
            Console.WriteLine("Incorrect scale.");
        }

        public WrongScaleException(string message) : base(message){}
    }
}