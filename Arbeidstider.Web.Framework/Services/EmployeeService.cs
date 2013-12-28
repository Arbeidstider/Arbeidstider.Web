using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.DataAccess.Repository;
using Arbeidstider.DataAccess.Repository.Constants.StoredProcedures;
using Arbeidstider.DataAccess.Repository.Exceptions;
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


        public bool CreateEmployee(string username, Guid userID, string lastname, string firstname, string mobile,
                                   string birthDate, int workplaceID)
        {
            var parameters = Parameters.Employee.Create(username, userID, lastname, firstname, mobile, birthDate,
                                                        workplaceID);
            try
            {
                _repository.Create(parameters);
                return true;
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

        public EmployeeDTO GetEmployee(Guid? userID)
        {
            var employee = (IEmployee)(object) new {UserID = userID};
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

        public IEnumerable<EmployeeDTO> GetAllEmployees(int workplaceID)
        {
            /* REFACTOR: */
            var parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("WorkplaceID", workplaceID));

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