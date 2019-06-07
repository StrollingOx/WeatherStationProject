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
        private enum SensorType {TemperatureSensor, HumiditySensor, PressureSensor, Sensor}
        private enum DegreeScale {Celsius, Fahrenheit}
        private static WeatherStation _station;
        private static Measurement _measurement;
        private static System.Timers.Timer _timer;
        
        public static void Main(string[] args)
        {
            /*** Creating and populating WeatherStation ***/
            _station = new WeatherStation();
            PopulateStation();
            
            /*** Extracting the list of sensors that meet the given condition ***/
            Console.WriteLine("SearchAllSensors(..., s => ((TemperatureSensor) s).GetTemperature() >= 0.0) method test");
            List<Sensor> sensorsWithTemperatureHigherThanZeroDegrees = 
                _station.SearchAllSensors(
                    (int) SensorType.TemperatureSensor,
                    s => ((TemperatureSensor) s).GetTemperature() >= 0.0);
            //Printing the data...
            foreach (var sensor in sensorsWithTemperatureHigherThanZeroDegrees) Console.WriteLine(sensor.ToString());
            
            /*** Measurement class test***/
            Console.WriteLine("\nMeasurement class test");
            _measurement = new Measurement();
            _measurement.AddRecord(new Sensor("testSensor1"), 0.0);                     //Sensor8
            _measurement.AddRecord(new PressureSensor(), 100.0);                               //Sensor9
            _measurement.AddRecord(new TemperatureSensor((int)DegreeScale.Celsius, 90));  //Sensor10

            Double sensorMeasurement = _measurement.GetRecord("Sensor9_measure_2");
            Console.WriteLine("DICTIONARY_TEST(Sensor9): "+sensorMeasurement);

            sensorMeasurement = _measurement.GetRecord("testSensor1_measure_1");
            Console.WriteLine("DICTIONARY_TEST(testSensor1): "+sensorMeasurement);
            
            sensorMeasurement = _measurement.GetRecord("Sensor10", 3);
            Console.WriteLine("DICTIONARY_TEST(Sensor10): "+sensorMeasurement);
           
            /*** Event test ***/
            Console.WriteLine("\nSensors event test");
            Sensor eventSensor = new Sensor(); //Sensor11
            
            
            
            
            
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
            (sensor4 as TemperatureSensor).SetScale((int)DegreeScale.Fahrenheit);
            Sensor sensor5 = new TemperatureSensor((int)DegreeScale.Celsius, 100);
            Sensor sensor6 = new TemperatureSensor((int)DegreeScale.Celsius, 80);
            Sensor sensor7 = new TemperatureSensor((int)DegreeScale.Celsius, -3);
            
            _station.AddSensor(sensor1);
            _station.AddSensor(sensor2);
            _station.AddSensor(sensor3);
            _station.AddSensor(sensor4);
            _station.AddSensor(sensor5);
            _station.AddSensor(sensor6);
            _station.AddSensor(sensor7);
        }
        private static void GenerateReport()
        {
            var someRam = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(_station.GetType());
            
            serializer.WriteObject(someRam,_station);
            someRam.Position = 0;
            StreamReader sr = new StreamReader(someRam);
            string jsonString = sr.ReadToEnd();

            
            var fileName = @"C:\Users\zai-2\Desktop\Reports\report"+
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