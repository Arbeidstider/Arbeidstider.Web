using System.Collections.Generic;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Interfaces.Services;

namespace Arbeidstider.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<Employer> _repository; 
        public UserService(IRepository<Employer> repository)
        {
            _repository = repository;
        }

        public bool VerifyUser(List<KeyValuePair<string, object>> parameters)
        {
            if (parameters[0].Value == null || parameters[1].Value == null) return false;
            return _repository.Verify(parameters);
        }
    }
}
