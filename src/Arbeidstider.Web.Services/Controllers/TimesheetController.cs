using System;
using System.Linq;
using System.Web.Mvc;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Common.DTO;
using Arbeidstider.Common.Enums;
using Arbeidstider.Common.Parameters;
using Arbeidstider.Web.Framework.ViewModels.Account;
using Arbeidstider.Web.Framework.ViewModels.Timesheet;
using Arbeidstider.Web.Framework.Helpers;

namespace Arbeidstider.Web.Services.Controllers
{
    public class TimesheetController : BaseServiceController
    {
        private readonly IRepository<Timesheet> _repository;

        public TimesheetController(IRepository<Timesheet> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public JsonResult GetAllTimesheets(TimesheetDTO timesheet)
        {
            var parameters = new TimesheetParameters(new Timesheet()
            {
                StartDate = DateTime.Parse(timesheet.StartDate),
                EndDate = DateTime.Parse(timesheet.EndDate),
                EmployeeID = timesheet.EmployeeID
            }, RepositoryAction.GetAll).Parameters;

            var timesheets = _repository.GetAll(parameters);

            var timesheetDTOs = timesheets.Select(x => new TimesheetDTO(x)).ToArray();
            return Json(timesheetDTOs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateTimesheet(TimesheetDTO timesheet)
        {
            var result = new JsonResult();
            /*
            if (!CurrentUser.HasAccessToEmployee(timesheet.EmployeeID))
            {
                result.Data = new {Result = false};
                return result;
            }
             */

            var parameters = new TimesheetParameters(new CreateTimesheet(timesheet), RepositoryAction.Create).Parameters;

            result.Data = new
            {
                Data = _repository.Create(parameters),
                Result = true
            };

            return result;
        }
    }
}