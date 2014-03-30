using System;

namespace Arbeidstider.Repository.Exceptions
{
    public class ScheduleEventRepositoryException : Exception
    {
        public ScheduleEventRepositoryException(string message) : base(message)
        {
        }
    }
}
