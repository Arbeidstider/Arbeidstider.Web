using System;

namespace Arbeidstider.Business.Logic.Repository.Exceptions
{
    public class EmployeeRepositoryException : Exception
    {
        public EmployeeRepositoryException(string message) : base(message)
        {
            
        }
    }
}
