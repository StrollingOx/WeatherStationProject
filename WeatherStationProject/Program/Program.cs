using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Timers;


namespace Program
{
    internal class Program
    {

        private static WeatherStation _station;
        private static System.Timers.Timer _timer;

        public static void Main(string[] args)
        {
            //TODO: Descriptions 
//           
            _station = new WeatherStation();
            PopulateStation();
            SetTimer();
            Console.WriteLine("\nPress the Enter key to exit the application...\n");
            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            Console.ReadLine();
            _timer.Stop();
            _timer.Dispose();
            
            Console.WriteLine("Terminating the application...");
        }

        private static void SetTimer()
        {
            _timer = new System.Timers.Timer(60000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Generating Report...");
            GenerateReport();
            Console.WriteLine("Success! Next report in {0}s.", _timer.Interval);
        }
        private static void PopulateStation()
        {
            Sensor sensor1 = new Sensor("Awesome sensor");
            Sensor sensor2 = new TemperatureSensor();
            sensor2.Name = "OOF sensor";
            Sensor sensor3 = new HumiditySensor();
            Sensor sensor4 = new TemperatureSensor();
            (sensor4 as TemperatureSensor).SetScale(1);

            Console.WriteLine(sensor4.ToString());
            
            _station.AddSensor(sensor1);
            _station.AddSensor(sensor2);
            _station.AddSensor(sensor3);
            _station.AddSensor(sensor4);
        }
        private static void GenerateReport()
        {
            var someRam = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(_station.GetType());
            
            serializer.WriteObject(someRam,_station);
            someRam.Position = 0;
            StreamReader sr = new StreamReader(someRam);
            string jsonString = sr.ReadToEnd();

            
            var fileName = @"C:\Users\Kuba\Desktop\RapTorts\report"+
                           DateTime.Now.ToString("--dd-MM-yyyyTHHmm")
                           +".json";
            Console.WriteLine(fileName);
            
            //TODO: Why are we using 'using' here?
            using (StreamWriter file = File.CreateText(fileName))
            {
                file.Write(jsonString);
            }
        }
    }
}

/***** OLD BUT GOLD XD *****/
//
//            var serializer = new DataContractSerializer(station.GetType());
//            var someRam = new MemoryStream();
//            serializer.WriteObject(someRam, station);
//            someRam.Seek(0, SeekOrigin.Begin);
//            Console.WriteLine(
//                XElement.Parse(
//                Encoding.ASCII.GetString(someRam.GetBuffer()).Replace("\0", "")));