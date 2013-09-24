using System.Collections.Generic;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Interfaces.Services;
using Arbeidstider.Business.Logic.IoC;

namespace Arbeidstider.Web.Framework.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<Employer> _repository; 
        private static IUserService _instance; 
        public UserService(IRepository<Employer> repository)
        {
            _repository = repository;
        }

        public static IUserService Instance
        {
            get
            {
                if (_instance == null) 
                    _instance =  new UserService(Container.Resolve<IRepository<Employer>>());

                return _instance;
            }
        }

        public bool VerifyUser(List<KeyValuePair<string, object>> parameters)
        {
            if (parameters[0].Value == null || parameters[1].Value == null) return false;
            return _repository.Verify(parameters);
        }
    }
}