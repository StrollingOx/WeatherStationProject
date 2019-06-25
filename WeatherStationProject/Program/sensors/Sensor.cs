using System;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Security.AccessControl;
using System.Text;

namespace Program
{
    [DataContract]
    public class Sensor :EventArgs, IDisposable
    {
        public delegate void RegisterMeasurementEventHandler(object o, Measurement e);

        public event RegisterMeasurementEventHandler MeasurementRegistered;
        private Measurement _measurement;
        
        public Measurement Measurement => _measurement;
        private string _name;
        [DataMember(Name="Sensor's Name")]
        public string Name
        {
            get => _name;
            set
            {
                if (value.Length > 16)
                    throw new NameOutOfScopeException();
                else
                    _name = value;
                
            }
        }
        private static int _number = 0;
        public static int Number
        {
            get { return _number; }
        }
        private bool disposed = false;

        public Sensor()
        {
            _number++;
            _name = "Sensor" + _number;
            _measurement = new Measurement();
        }

        public Sensor(string name)
        {
            _number++;
            Name = name;
            _measurement = new Measurement();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return; 
      
            if (disposing)
            {
                _number--;
                _measurement = null; 
            }
      
            disposed = true;
        }

        ~Sensor()
        {
            Dispose(false);
            _number--;
        }
        
        public override string ToString()
        {
            return Name + ": ONLINE.";
        }

        //EVENTS & MEASUREMENTS--------------------------------------

        public void RegisterCurrentMeasure()
        {
            _measurement.AddRecord(this);
            OnMeasurementRegistered(_measurement);
        }
        
        public void RegisterCurrentMeasure(double value)
        {
            _measurement.AddRecord(this, value);
        }
        
        protected virtual void OnMeasurementRegistered(Measurement measurement)
        {
            MeasurementRegistered?.Invoke(this, measurement );
        }

        public void SendDataToServer()
        {
            try 
            {
            Int32 port = 9759;
            TcpClient client = new TcpClient("127.0.0.1", port);

            String message = _name+"-"
                                  +_name+"_measure_1-"
                                  +_measurement.GetRecord(_name, 1);
            
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message); 
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            
            stream.Close();         
            client.Close(); 
            } 
            catch (ArgumentNullException e) 
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            } 
            catch (SocketException e) 
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }
    }

    public class NameOutOfScopeException : Exception
    {
        public NameOutOfScopeException()
        {
            Console.WriteLine("Name is too long!!! (Less than 16 characters)");
        }
        public NameOutOfScopeException(string message) : base(message){}
    }
    
}