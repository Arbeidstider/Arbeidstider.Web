using System;

namespace Arbeidstider.Business.Esceptions.Repository
{
    public class EmployerRepositoryException : Exception
    {
        public EmployerRepositoryException(string message) : base(message) { }
    }
}