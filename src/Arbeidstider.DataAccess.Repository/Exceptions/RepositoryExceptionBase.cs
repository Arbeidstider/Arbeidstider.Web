using System;

namespace Arbeidstider.DataAccess.Repository.Exceptions
{
    public class RepositoryExceptionBase : Exception
    {
        public RepositoryExceptionBase(string message) : base(message)
        {
            
        }
    }
}
