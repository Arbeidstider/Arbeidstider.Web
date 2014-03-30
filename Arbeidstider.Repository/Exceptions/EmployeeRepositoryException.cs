using System;

namespace Arbeidstider.Repository.Exceptions
{
    public class EmployeeRepositoryException : Exception
    {
        public EmployeeRepositoryException(string message) : base(message)
        {
            
        }
    }
}
