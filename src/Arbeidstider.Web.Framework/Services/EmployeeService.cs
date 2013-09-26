using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.IoC;

namespace Arbeidstider.Web.Framework.Services
{
    public class EmployeeService
    {
        private readonly static EmployeeService _instance;
        private readonly IRepository<Employee> _repository;

        private EmployeeService(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        public static EmployeeService Instance
        {
            get
            {
                if (_instance == null)
                    return new EmployeeService(Container.Resolve<IRepository<Employee>>());

                return _instance;
            }
        }
    }
}
