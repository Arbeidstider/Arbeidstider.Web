using System;

namespace Arbeidstider.Repository.Exceptions
{
    public class RepositoryExceptionBase : Exception
    {
        public RepositoryExceptionBase(string message) : base(message)
        {
            
        }
    }
}
