using System;

namespace Arbeidstider.Business.Logic.Repository.Exceptions
{
    public class ScheduleEventRepositoryException : Exception
    {
        public ScheduleEventRepositoryException(string message) : base(message)
        {
        }
    }
}
