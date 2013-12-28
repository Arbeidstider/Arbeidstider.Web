using System;

namespace Arbeidstider.DataAccess.Repository.Exceptions
{
    public class ScheduleEventRepositoryException : Exception
    {
        public ScheduleEventRepositoryException(string message) : base(message)
        {
        }
    }
}
