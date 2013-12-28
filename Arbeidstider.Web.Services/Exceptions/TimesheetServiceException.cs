using System;

namespace Arbeidstider.Web.Services.Exceptions
{
    public class TimesheetServiceException : Exception
    {
        public TimesheetServiceException(string message) : base(message)
        {
        }
    }
}