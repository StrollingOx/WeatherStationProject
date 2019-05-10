using System;
using System.Security.AccessControl;

namespace Program
{
    public class Sensor :IDisposable
    {
        private string _name;

        public string Name
        {
            get { return _name; }
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
        }

        public Sensor(string name)
        {
            if (name.Length > 16)
                throw new NameOutOfScopeException();
            else
            {
                _name = name;
                _number++;
            }

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
            }
      
            disposed = true;
        }

        ~Sensor()
        {
            Dispose(false);
            _number--;
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