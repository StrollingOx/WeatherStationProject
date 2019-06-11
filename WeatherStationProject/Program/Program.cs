using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Timers;


namespace Program
{
    internal class Program
    {
        private static System.Timers.Timer _timer;
        
        public static void Main(string[] args)
        {
            WeatherStation s1= new WeatherStation("King's Cross Station");
            WeatherStation s2 = new WeatherStation("Marylebone Station");
            WeatherStation s3 = new WeatherStation("Fenchurch Street Station");
            WeatherStation s4 = new WeatherStation("Liverpool Street Station");
            
            /*** Multiple station event test ***/
            Sensor eventSensor = new HumiditySensor(); //Sensor11
            ((HumiditySensor)eventSensor).SetHumidity(38.52);
            eventSensor.MeasurementRegistered += s1.OnMeasurementRegistered;
            eventSensor.MeasurementRegistered += s2.OnMeasurementRegistered;
            eventSensor.MeasurementRegistered += s3.OnMeasurementRegistered;
            eventSensor.MeasurementRegistered += s4.OnMeasurementRegistered;
            eventSensor.RegisterCurrentMeasure();
            
            
            
            
            
            /*** Generating reports ***/
            SetTimer(60);
            EnableTimer(false);
            Console.WriteLine("\nPress the Enter key to exit the application...\n");
            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            Console.ReadLine();
            _timer.Stop();
            _timer.Dispose();
            
            Console.WriteLine("Terminating the application...");
        }

       
        private static void SetTimer(int interval)
        {
            _timer = new System.Timers.Timer(interval*1000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = false;
        }

        private static void EnableTimer(bool online)
        {
            _timer.Enabled = online;
        }
        
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Generating Report...");
//            GenerateReport();
            Console.WriteLine("Success! Next report in {0}s.", _timer.Interval);
        }
        
//        private static void GenerateReport()
//        {
//            var someRam = new MemoryStream();
//            DataContractJsonSerializer serializer = new DataContractJsonSerializer(_station.GetType());
//            
//            serializer.WriteObject(someRam,_station);
//            someRam.Position = 0;
//            StreamReader sr = new StreamReader(someRam);
//            string jsonString = sr.ReadToEnd();
//
//            
//            var fileName = @"C:\Users\zai-2\Desktop\Reports\report"+
//                           DateTime.Now.ToString("--dd-MM-yyyyTHHmm")
//                           +".json";
//            Console.WriteLine(fileName);
//            
//            //TODO: Why are we using 'using' here?
//            using (StreamWriter file = File.CreateText(fileName))
//            {
//                file.Write(jsonString);
//            }
//        }
    }
}
