using System;

namespace Arbeidstider.DataAccess.Repository.Exceptions
{
    public class TimesheetRepositoryException : Exception
    {
        public TimesheetRepositoryException(string message) : base(message)
        {
            
        }
    }
}