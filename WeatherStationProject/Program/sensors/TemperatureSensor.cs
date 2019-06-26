using System;
using System.Runtime.Serialization;

namespace Program
{
    [DataContract]
    public class TemperatureSensor: Sensor, ITemperature
    {
        [DataMember(Name="Scale")]
        private string _scale = ""; //0 = Celsius; 1 = Fahrenheit
        [DataMember(Name="Degrees")]
        private double _temperature = 0.0d;

        public TemperatureSensor()
        {
            _scale = "Celsius";
        }

        public TemperatureSensor(int scale)
        {
            SetScale(scale);
        }
        
        public TemperatureSensor(int scale, double temperature)
        {
            SetScale(scale);
            _temperature = temperature;
        }
        
        public void SetScale(int scale)
        {
            switch (scale)
            {
                case (int)DegreeScale.Celsius:
                    if (_scale.Equals("Fahrenheit"))
                    {
                        ConvertToCelsius();
                    }
                    _scale = "Celsius";
                    break;
                case (int)DegreeScale.Fahrenheit: 
                    if (_scale.Equals("Celsius"))
                    {
                        ConvertToFahrenheit();
                    }
                    _scale = "Fahrenheit";
                    break;
                default: throw new WrongScaleException();
                    
            }
        
        }

        private void ConvertToCelsius()
        {
            _temperature = (_temperature - 32) * 5.0 / 9.0;
        }
        
        private void ConvertToFahrenheit()
        {
            _temperature = (_temperature * 9.0 / 5.0) + 32;
        }

        public string GetScaleName()
        {
            return _scale;
        }

        public string GetScaleSymbol()
        {
            return "°" + _scale.Substring(0, 1);
        }

        public double GetTemperature()
        {
            return _temperature;
        }
        
        public void SetTemperature(double value)
        {
            _temperature = value;
        }

        public override string ToString()
        {
            return Name + ": " + GetTemperature() + GetScaleSymbol();
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