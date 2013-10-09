using System;
using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.UnitTests.BaseClasses;
using NUnit.Framework;

namespace Arbeidstider.Web.UnitTests.Repositories
{
    public class EmployeeRepositoryTests : TestBase
    {
        private IRepository<IEmployee> _repository;

        public EmployeeRepositoryTests() : base()
        {
        }

        [TestCase]
        public void GetAll()
        {
            Setup();
            _repository = IoC.Container.Resolve<IRepository<IEmployee>>();
            var dto = TimesheetDTO.Create(startDate: new DateTime(2013, 09, 30), userID: new Guid());
            _repository.GetAll(dto.Parameters());
        }
    }
}
