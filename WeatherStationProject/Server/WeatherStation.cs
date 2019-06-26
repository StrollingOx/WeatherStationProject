using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Serialization;

namespace Program
{
    [DataContract]
    [KnownType(typeof(TemperatureSensor))]
    [KnownType(typeof(PressureSensor))]
    [KnownType(typeof(HumiditySensor))]


    public class WeatherStation
    {
        [DataMember(Name = "List of sensor")] 
        private readonly List<Sensor> _sensors;
        public WeatherStation()
        {
            _sensors = new List<Sensor>();
        }
        
        public void AddSensor(Sensor sensor)
        {
            _sensors.Add(sensor);
        }
 

        public void ExecuteServer()
        {
            Int32 port = 9759;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(localAddr, port);
            server.Start();
            
            while(true) 
            {
                Console.Write("Waiting for a connection... ");
        
                TcpClient client = server.AcceptTcpClient();            
                Console.WriteLine("Connected!");
                
                Thread t = new Thread(new ParameterizedThreadStart(GetDataFromClient));
                t.Start(client);

                
            }
        }

        private void GetDataFromClient(object o)
        {
            var client = (TcpClient)o;
            var bytes = new byte[256];
            var stream = client.GetStream();
            
            int i;
            while((i = stream.Read(bytes, 0, bytes.Length))!=0) 
            {   
                var sensorData = Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", sensorData);
                var data = sensorData.Split('-');
                int type = Convert.ToInt32(data[3]);
                Sensor sensor;
                
                if (type == (int) SensorType.TemperatureSensor)
                {
                    sensor = new TemperatureSensor();
                    (sensor as Sensor).Name = data[0];
                    ((TemperatureSensor) sensor).SetTemperature(Convert.ToDouble(data[2]));
                }
                else if (type == (int) SensorType.HumiditySensor)
                {

                    sensor = new HumiditySensor();
                    (sensor as Sensor).Name = data[0];
                    ((HumiditySensor) sensor).SetHumidity(Convert.ToDouble(data[2]));
                }
                else if (type == (int) SensorType.PressureSensor)
                {
                    sensor = new PressureSensor();
                    (sensor as Sensor).Name = data[0];
                    ((PressureSensor) sensor).SetPressure(Convert.ToDouble(data[2]));
                }
                else
                {
                    sensor = new Sensor(data[0]);
                    sensor.RegisterCurrentMeasure(Convert.ToDouble(data[2]));
                    _sensors.Add(sensor);
                }


            }
            stream.Close();
            client.Close();
        }
        
    }
}