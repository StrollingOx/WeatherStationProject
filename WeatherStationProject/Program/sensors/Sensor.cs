using System;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.Serialization;
using System.Security.AccessControl;

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
        
        protected virtual void OnMeasurementRegistered(Measurement measurement)
        {
            MeasurementRegistered?.Invoke(this, measurement );
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