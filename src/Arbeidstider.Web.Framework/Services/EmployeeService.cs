using System;
using System.Collections.Generic;
using Arbeidstider.Business.Interfaces.Caching;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Caching;
using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Business.Logic.IoC;
using Arbeidstider.Common.Parameters;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.ViewModels.Account;

namespace Arbeidstider.Web.Framework.Services
{
    public class EmployeeService
    {
        private readonly ICacheService _cacheService; 
        private readonly IRepository<Employee> _repository; 
        private static EmployeeService _instance; 
        public static EmployeeService Instance
        {
            get
            {
                if (_instance == null) 
                    _instance =  new EmployeeService(IoC.Resolve<IRepository<Employee>>(), IoC.Resolve<ICacheService>());

                return _instance;
            }
        }

        private EmployeeService(IRepository<Employee> repository, ICacheService cacheservice)
        {
            _cacheService = cacheservice;
            _repository = repository;
        }

        public EmployeeUser GetEmployee(string username)
        {
            if (username == null) return null;

            var employee = _cacheService.Get(CacheKeys.GetEmployee, () =>
                _repository.Get(new UserParameters(username).Parameters), DateTime.UtcNow.AddHours(8));

            return new EmployeeUser()
            {
                EmployeeID = employee.EmployeeID,
                Passwordhash = employee.Passwordhash,
                Username = employee.Username
            };
        }

        public bool UpdateEmployee(EmployeeDTO dto, Guid userID)
        {
            dto.UserID = userID;
            var parameters = new EmployeeParameters(dto, RepositoryAction.Update).Parameters;
            return _repository.Update(parameters);
        }

        public IEnumerable<Employee> GetAllEmployees(List<KeyValuePair<string, object>> parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}