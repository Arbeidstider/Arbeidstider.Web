using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.DataAccess.Repository.Exceptions;
using Arbeidstider.DataAccess.Repository.Parameters;
using Arbeidstider.Interfaces;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Session;

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


        public int CreateEmployee(int userId, int workplaceId)
        {
            var parameters = EmployeeParameters.Create(userId: userId, workplaceId: workplaceId);
            try
            {
                return _repository.Create(parameters);
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return 0;
            }
        }

        public EmployeeDTO GetEmployeeByUserId(int userId, bool getTiny = false)
        {
            var parameters = EmployeeParameters.Create(userId: userId);
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

        public EmployeeDTO GetEmployee(int employeeId, bool getTiny = false)
        {
            var parameters = EmployeeParameters.Create(employeeId: employeeId);
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
            var parameters = EmployeeParameters.Create(username: username);

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
            var parameters = EmployeeParameters.Create(workplaceId: workplaceId);

            try
            {
                return
                    (from x in _repository.GetAll(parameters)
                     select new EmployeeDTO(x)).
                     ToArray();
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