using System;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.Serialization;
using System.Security.AccessControl;

namespace Program
{
    public delegate void ChangedEventHandler(object sender, EventArgs e);
    [DataContract]
    public class Sensor :IDisposable
    {
        public event ChangedEventHandler Changed;
        private Measurement _measurement;
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
                Changed = null;
                /* Is thise enoguh? */
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

        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        public void AddMeasure()
        {
            _measurement.AddRecord(this, 111.11);
            OnChanged(EventArgs.Empty);
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