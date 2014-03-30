using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.Repository;
using Arbeidstider.Repository.Exceptions;
using Arbeidstider.Repository.Parameters;
using Arbeidstider.DataInterfaces;
using Arbeidstider.DataInterfaces.Repository;
using Arbeidstider.DataObjects.DAO;
using Arbeidstider.DataObjects.DTO;
using ServiceStack;

namespace Arbeidstider.DataServices
{
//public sealed class UserRepository : Repository<User>, IUserRepository
//{
    //public UserRepository() : base("Users") { }
 
    //internal override dynamic Mapping(User item)
    //{
    //    return new
    //    {
    //        ID = item.ID,
    //        Username = item.Username,
    //        Password = item.Password.EncryptedValue
    //    };
    //}
//}
    public class EmployeeService : ServiceBase
    {
        private static EmployeeService _instance;
        private EmployeeRepository _repository;

        public static EmployeeService Instance
        {
            get
            {
                if (_instance == null)
                {
                    var repository = new EmployeeRepository();
                    _instance = new EmployeeService(repository);
                }
                    //_instance = new EmployeeService(_repository);

                return _instance;
            }
        }
        private EmployeeService()
        {
            
        }
        public EmployeeService(EmployeeRepository repository)
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
                return new EmployeeDTO((IEmployee)obj);
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        public EmployeeDTO GetEmployee(int? employeeId = null, int? userId = null, bool getTiny = false)
        {
            var parameters = EmployeeParameters.Create(employeeId: employeeId, userId:userId);
            try
            {
                var obj = _repository.Get(parameters);
                return new EmployeeDTO((IEmployee)obj);
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
                return new EmployeeDTO((IEmployee)_repository.Get(parameters));
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