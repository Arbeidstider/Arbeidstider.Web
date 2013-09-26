using System.Collections.Generic;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.IoC;
using Arbeidstider.Web.Framework.ViewModels.Account;

namespace Arbeidstider.Web.Framework.Services
{
    public class UserService
    {
        private readonly IRepository<Employee> _repository; 
        private static UserService _instance; 
        public static UserService Instance
        {
            get
            {
                if (_instance == null) 
                    _instance =  new UserService(Container.Resolve<IRepository<Employee>>());

                return _instance;
            }
        }

        private UserService(IRepository<Employee> repository)
        {
            _repository = repository;
        }



        public EmployeeUser VerifyUser(List<KeyValuePair<string, object>> parameters)
        {
            if (parameters[0].Value == null || parameters[1].Value == null) return null;
            if (!_repository.Exists(parameters)) return null;

            var employee = _repository.Get(new List<KeyValuePair<string, object>>() {parameters[0]});
            var user = new EmployeeUser()
            {
                EmployeeID = employee.EmployeeID,
                Passwordhash = employee.Passwordhash,
                Username = employee.Username
            };

            return user;
        }
    }
}