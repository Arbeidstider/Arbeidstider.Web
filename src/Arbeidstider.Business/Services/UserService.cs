using System.Collections.Generic;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Interfaces.Services;
using Arbeidstider.Business.Repository;

namespace Arbeidstider.Business.Services
{
    public class UserService : IUserService
    {
        private static IUserService _instance;
        private readonly IRepository<Employer> _repository; 

        public static IUserService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserService();

                return _instance;
            }
        }

        private UserService()
        {
            _repository = EmployerRepository.Instance;
        }

        public bool VerifyUser(List<KeyValuePair<string, object>> parameters)
        {
            if (parameters[0].Value == null || parameters[1].Value == null) return false;
            return _repository.Verify(parameters);
        }
    }
}
