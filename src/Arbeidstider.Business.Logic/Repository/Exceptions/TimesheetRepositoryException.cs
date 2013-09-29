using System;

namespace Arbeidstider.Business.Logic.Repository.Exceptions
{
    public class TimesheetRepositoryException : Exception
    {
        public TimesheetRepositoryException(string message) : base(message)
        {
            
        }
    }
}