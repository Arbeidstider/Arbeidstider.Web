using System.Collections.Generic;
using System.Linq;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Caching;
using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Business.Logic.Repository.Exceptions;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Parameters;
using Arbeidstider.Web.Framework.ViewModels.Account;

namespace Arbeidstider.Web.Framework.Services
{
    public class EmployeeService : ServiceBase
    {
        private readonly IRepository<Employee> _repository;
        private static EmployeeService _instance; 
        public static EmployeeService Instance
        {
            get
            {
                if (_instance == null) 
                    _instance =  new EmployeeService(IoC.Resolve<IRepository<Employee>>());

                return _instance;
            }
        }

        private EmployeeService(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        public EmployeeUser GetEmployee(string username)
        {
            try
            {
                return Cache.Get(CacheKeys.GetEmployee, 
                    () => ParseEmployee(_repository.Get(new UserParameters(username).Parameters)));
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        public EmployeeDTO UpdateEmployee(EmployeeDTO dto, string username)
        {
            try
            {
                if (!_repository.Update(new EmployeeParameters(dto, RepositoryAction.Update).Parameters))
                    Logger.Error(string.Format("Couldn´t update employee with username: {0}", username));

                    return new EmployeeDTO(_repository.Get(new EmployeeParameters(dto, RepositoryAction.Get).Parameters));
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        public IEnumerable<EmployeeUser> GetAllEmployees(int workplaceID)
        {
            try
            {
                return Cache.Get(CacheKeys.GetAllEmployees, 
                    () => ParseEmployees(_repository.GetAll(new EmployeeParameters(new EmployeeDTO() {WorkplaceID = workplaceID}, RepositoryAction.GetAll).Parameters)));
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        #region Private Methods
        private static EmployeeUser ParseEmployee(Employee employee)
        {
            return new EmployeeUser(employee);
        }

        private static IEnumerable<EmployeeUser> ParseEmployees(IEnumerable<Employee> employees)
        {
            return employees.Select(x => new EmployeeUser(x)).ToArray();
        }
        #endregion
    }
}