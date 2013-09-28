using System;
using System.Collections.Generic;
using Arbeidstider.Business.Interfaces.Repository;
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
        private readonly IRepository<Employee> _repository; 
        private static EmployeeService _instance; 
        public static EmployeeService Instance
        {
            get
            {
                if (_instance == null) 
                    _instance =  new EmployeeService(Container.Resolve<IRepository<Employee>>());

                return _instance;
            }
        }

        private EmployeeService(IRepository<Employee> repository)
        {
            _repository = repository;
        }



        public EmployeeUser GetEmployee(List<KeyValuePair<string, object>> parameters)
        {
            if (parameters[0].Value == null) return null;

            var employee = _repository.Get(new List<KeyValuePair<string, object>>() {parameters[0]});
            var user = new EmployeeUser()
            {
                EmployeeID = employee.EmployeeID,
                Passwordhash = employee.Passwordhash,
                Username = employee.Username
            };

            return user;
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