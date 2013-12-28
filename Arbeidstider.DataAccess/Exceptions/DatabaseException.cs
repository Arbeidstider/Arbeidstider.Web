using System;

namespace Arbeidstider.Database.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message)
        {
            
        }
    }
}
