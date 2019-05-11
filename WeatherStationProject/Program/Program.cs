using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;


namespace Program
{
    internal class Program
    {
        
        public static void Main(string[] args)
        {
            //TODO: Descriptions 
            WeatherStation station = new WeatherStation();
//            var autoEvent = new AutoResetEvent(false);
            
            
           
            Sensor sensor1 = new Sensor("Awesome sensor");
            Sensor sensor2 = new TemperatureSensor();
            sensor2.Name = "OOF sensor";
            Sensor sensor3 = new HumiditySensor();
            Sensor sensor4 = new TemperatureSensor();
            (sensor4 as TemperatureSensor).SetScale(1);
            
            station.AddSensor(sensor1);
            station.AddSensor(sensor2);
            station.AddSensor(sensor3);
            station.AddSensor(sensor4);
            
            GenerateReport(station);
                       
//            var serializer = new DataContractSerializer(station.GetType());
//            var someRam = new MemoryStream();
//            serializer.WriteObject(someRam, station);
//            someRam.Seek(0, SeekOrigin.Begin);
//            Console.WriteLine(
//                XElement.Parse(
//                Encoding.ASCII.GetString(someRam.GetBuffer()).Replace("\0", "")));

        }

        private static void GenerateReport(WeatherStation station)
        {
            var someRam = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(station.GetType());
            
            serializer.WriteObject(someRam,station);
            someRam.Position = 0;
            StreamReader sr = new StreamReader(someRam);
            string jsonString = sr.ReadToEnd();

            //TODO: Why are we using 'using' here?
            var fileName = @"C:\Users\Anon\Desktop\report"+
                           DateTime.Now.ToString("--dd-MM-yyyyTHHmm")
                           +".json";
            Console.WriteLine(fileName);
            using (StreamWriter file = File.CreateText(fileName))
            {
                file.Write(jsonString);
            }
        }
    }
}