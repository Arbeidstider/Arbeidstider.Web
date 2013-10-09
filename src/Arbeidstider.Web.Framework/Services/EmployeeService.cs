using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Caching;
using Arbeidstider.Business.Logic.Repository.Exceptions;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.ViewModels.Account;

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


        public bool CreateEmployee(EmployeeDTO dto)
        {
            try
            {
                _repository.Create(dto.Parameters());
                InvalidateEmployeeCache();
                return true;
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

        public IEmployeeUser GetEmployee(Guid userID)
        {
            try
            {
                return Cache.Get(CacheKeys.GetEmployee,
                    () => new EmployeeUser(_repository.Get(EmployeeDTO.Create(userID: userID).Parameters())));
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        public IEmployeeUser GetEmployee(string username)
        {
            try
            {
                return Cache.Get(CacheKeys.GetEmployee,
                    () => new EmployeeUser(_repository.Get(EmployeeDTO.Create(username: username).Parameters())));
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        public IEnumerable<IEmployeeUser> GetAllEmployees(int workplaceID)
        {
            try
            {
                return Cache.Get(CacheKeys.GetAllEmployees,
                    () => (from x in _repository.GetAll(EmployeeDTO.Create(workplaceID: workplaceID).Parameters())
                        select new EmployeeUser(x)).ToArray());
            }
            catch (EmployeeRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        public bool UpdateEmployee(Guid userID, string username)
        {
            try
            {
                if (!_repository.Update(EmployeeDTO.Create(userID: userID, username: username).Parameters()))
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

        private static void InvalidateEmployeeCache()
        {
            Cache.Invalidate(CacheKeys.GetAllEmployees);
            Cache.Invalidate(CacheKeys.GetEmployee);
        }
    }
}