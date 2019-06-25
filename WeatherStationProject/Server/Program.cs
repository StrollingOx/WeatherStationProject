using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Timers;
using Program;

namespace Server
{
    internal class Program
    {
        private static System.Timers.Timer _timer;
        private static WeatherStation _station;
        
        public static void Main(string[] args)
        {
            _station = new WeatherStation();
            
            SetTimer(10);
            EnableTimer(true);
            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            _station.ExecuteServer();
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
            GenerateReport();
            Console.WriteLine("Success! Next report in {0}s.", _timer.Interval/1000);
        }
        
        private static void GenerateReport()
        {
            var someRam = new MemoryStream();
            var station = _station;
            
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(station.GetType());
            
            serializer.WriteObject(someRam, station);
            someRam.Position = 0;
            StreamReader sr = new StreamReader(someRam);
            string jsonString = sr.ReadToEnd();
            
            var fileName = @"C:\Users\Anon\Desktop\Reports\report"+
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