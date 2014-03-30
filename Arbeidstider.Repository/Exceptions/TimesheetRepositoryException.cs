using System;

namespace Arbeidstider.Repository.Exceptions
{
    public class TimesheetRepositoryException : Exception
    {
        public TimesheetRepositoryException(string message) : base(message)
        {
            
        }
    }
}