using System;

namespace Arbeidstider.DataAccess.Repository.Exceptions
{
    public class EmployeeRepositoryException : Exception
    {
        public EmployeeRepositoryException(string message) : base(message)
        {
            
        }
    }
}
