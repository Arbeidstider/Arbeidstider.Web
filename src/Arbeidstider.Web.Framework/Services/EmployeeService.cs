using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Arbeidstider.Cache;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.DataAccess.Repository;
using Arbeidstider.DataAccess.Repository.Constants.StoredProcedures;
using Arbeidstider.DataAccess.Repository.Exceptions;
using Arbeidstider.Web.Framework.ViewModels.Account;
using Arbeidstider.Web.Framework.ViewModels.Dashboard;

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


        public bool CreateEmployee(string username, Guid userID, string lastname, string firstname, string mobile, string birthDate, int workplaceID)
        {
            var parameters = Parameters.Employee.Create(username, userID, lastname, firstname, mobile, birthDate,
                                                        workplaceID);
            try
            {
                _repository.Create(parameters);
                InvalidateEmployeeCache(userID);
                return true;
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

        public EmployeeUser GetEmployee(Guid userID)
        {
            var parameters = Parameters.Employee.Get(userID);
            try
            {
                return Cache.Get(CacheKeys.GetEmployee,
                                 () => _getEmployee(parameters));
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        public EmployeeUser GetEmployee(string username)
        {
            var parameters = Parameters.Employee.Get(username);

            try
            {
                return Cache.Get(
                        CacheKeys.GetEmployee,
                        () => _getEmployee(parameters)
                    );
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        #region GetEmployee Callback
        private EmployeeUser _getEmployee(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return new EmployeeUser(_repository.Get(parameters));
        }
        #endregion

        public IEnumerable<EmployeeUser> GetAllEmployees(int workplaceID)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("WorkplaceID", workplaceID));
            try
            {
                return Cache.Get(
                    CacheKeys.GetAllEmployees,
                    () => _getAllEmployees(parameters));
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        #region GetAllEmployees Callback
        private IEnumerable<EmployeeUser> _getAllEmployees(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return
                (from x in _repository.GetAll(parameters)
                 select new EmployeeUser(x)).ToArray();
        }
        #endregion

        public bool UpdateEmployee(Guid userID, string username)
        {
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

                InvalidateEmployeeCache();

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
            return Membership.ValidateUser(userName, password);
        }

        public NewEmployee Register(NewEmployee employee)
        {
            var username = employee.GenerateUsername();
            var member = CreateUser(username, employee.GeneratePassword(), employee.Email);
            if (!CreateEmployee(
                username, 
                (Guid)member.ProviderUserKey,
                employee.Lastname,
                employee.Firstname,
                employee.Mobile,
                employee.BirthDate,
                employee.WorkplaceID
                ))
            {
                Logger.Warn(string.Format("Failed to create user with firstname: {0} and lastname: {1}", employee.Firstname, employee.Lastname));
            }

            employee.Success = true;

            return employee;
        }

        private static MembershipUser CreateUser(string username, string password, string email)
        {
            var userID = Guid.NewGuid();
            MembershipCreateStatus status;
            Membership.CreateUser(username, password, email, null, null, true, userID,
                                  out status);

            if (status != MembershipCreateStatus.Success)
            {
                throw new Exception(
                    string.Format("MembershipProvider failed to create user with username: {0} with status :{1}",
                                  username,
                                  status));
            }
            return Membership.GetUser(username);
        }

        private static void InvalidateEmployeeCache(Guid? userID = null)
        {
            Cache.Invalidate(CacheKeys.GetAllEmployees);
            Cache.Invalidate(CacheKeys.GetEmployee);
        }
    }
}   