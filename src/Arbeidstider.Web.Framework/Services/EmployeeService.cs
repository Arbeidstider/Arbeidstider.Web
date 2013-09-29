using System;
using System.Collections.Generic;
using Arbeidstider.Business.Interfaces.Caching;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Caching;
using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Business.Logic.Repository.Exceptions;
using Arbeidstider.Common.Parameters;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Parameters;
using Arbeidstider.Web.Framework.ViewModels.Account;
using log4net;

namespace Arbeidstider.Web.Framework.Services
{
    public class EmployeeService
    {
        private readonly ICacheService _cacheService; 
        private readonly IRepository<Employee> _repository;
        private readonly ILog Logger;
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
            Logger = IoC.Resolve<ILog>();
        }

        public EmployeeUser GetEmployee(string username)
        {
            try
            {
                return _cacheService.Get(CacheKeys.GetEmployee, 
                    () => ParseEmployee(_repository.Get(new UserParameters(username).Parameters)), 
                    DateTime.UtcNow.AddHours(8));
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        private EmployeeUser ParseEmployee(Employee employee)
        {
            EmployeeUser user = new EmployeeUser();

            user.EmployeeID = employee.EmployeeID;
            user.Passwordhash = employee.Passwordhash;
            user.Username = employee.Username;
            user.EmployeeGroup = employee.EmployeeGroup;

            return user;
        }

        public bool UpdateEmployee(EmployeeDTO dto, Guid userID)
        {
            dto.UserID = userID;
            var parameters = new EmployeeParameters(dto, RepositoryAction.Update).Parameters;
            return _repository.Update(parameters);
        }

        public IEnumerable<EmployeeDTO> GetAllEmployees(int workplaceID)
        {
            try
            {
                return _cacheService.Get(CacheKeys.GetAllEmployees, 
                    () => ParseEmployees(_repository.GetAll(new EmployeeParameters(new EmployeeDTO() {WorkplaceID = workplaceID}, RepositoryAction.GetAll).Parameters)), 
                    DateTime.Now.AddHours(8));
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        private IEnumerable<EmployeeDTO> ParseEmployees(IEnumerable<Employee> employees)
        {
            var list = new List<EmployeeDTO>();
            foreach (var employee in employees)
            {
                var dto = new EmployeeDTO();
                dto.EmployeeGroup = employee.EmployeeGroup;
                dto.EmployeeID = employee.EmployeeID;
                dto.Mobile = employee.Mobile;
                dto.UserID = employee.UserID;
                dto.Username = employee.Username;
                dto.WorkplaceID = employee.WorkplaceID;
                list.Add(dto);
            }

            return list;
        }
    }
}