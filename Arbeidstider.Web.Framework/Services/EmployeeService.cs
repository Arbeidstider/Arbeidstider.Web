using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.DataAccess.Repository.Exceptions;
using Arbeidstider.Interfaces;
using Arbeidstider.Web.Framework.DTO;

namespace Arbeidstider.Web.Framework.Services
{
    public class EmployeeService : ServiceBase
    {
        private static EmployeeService _instance;
        private readonly IRepository<IEmployee> _repository;

        public static EmployeeService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EmployeeService(IoC.Resolve<IRepository<IEmployee>>());

                return _instance;
            }
        }

        private EmployeeService(IRepository<IEmployee> repository)
        {
            _repository = repository;
        }


        public IEmployee CreateEmployee(int userId, int workplaceId)
        {
            try
            {
                var parameters = EmployeeParameters.Create(userId, workplaceId);
                return _repository.Create((IEmployee)(object)new { UserId = userId, WorkplaceId = workplaceId});
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                throw ex;
            }
        }

        public EmployeeDTO GetEmployee(int userId)
        {
            var employee = (IEmployee)(object) new {UserId = userId};
            var parameters = Parameters.Employee.Get(employee);
            try
            {
                var obj = _repository.Get(parameters);
                return new EmployeeDTO(obj);
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        public EmployeeDTO GetEmployee(string username)
        {
            var employee = (IEmployee)(object) new {Username = username};
            var parameters = Parameters.Employee.Get(employee);

            try
            {
                return new EmployeeDTO(_repository.Get(parameters));
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        public IEnumerable<EmployeeDTO> GetAllEmployees(int workplaceId)
        {
            var parameters = Parameters.Employee.GetAll((IEmployee) (object) new {WorkplaceId = workplaceId});

            try
            {
                return
                    (from x in _repository.GetAll(parameters)
                     select new EmployeeDTO(x)).ToArray();
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        public bool UpdateEmployee(Guid userID, string username)
        {
            //var parameters = Parameters.Employee.Update();
            /* REFACTOR : */
            var parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("UserID", userID));
            parameters.Add(new KeyValuePair<string, object>("Username", username));
        
            try
            {
                if (!_repository.Update(parameters))
                {
                    Logger.Error(string.Format("Couldn´t update employee with username: {0}", username));
                    return false;
                }

                return true;
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

        public bool ValidateEmployee(string userName, string password)
        {
            return true;
        }
    }
}   