using System.Linq;
using System.Web.Mvc;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Repository;
using Arbeidstider.Common.Enums;
using Arbeidstider.Web.Services.DTO;
using Arbeidstider.Web.Services.Models;
using Arbeidstider.Web.Services.Parameters;

namespace Arbeidstider.Web.Services.Controllers
{
    public class TimesheetController : BaseServiceController
    {
        private readonly IRepository<Timesheet> _repository;

        public TimesheetController()
        {
            _repository = TimesheetRepository.Instance;
        }

        [HttpGet]
        public JsonResult GetAllTimesheets(TimesheetDTO timesheet)
        {
            var parameters = new TimesheetParameters(timesheet, RepositoryAction.GetAll).Parameters;
            var timesheets = _repository.GetAll(parameters);

            var timesheetDTOs = timesheets.Select(x => new TimesheetDTO(x)).ToArray();
            return Json(timesheetDTOs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateTimesheet(TimesheetDTO timesheet)
        {
            var result = new JsonResult();
            if (!CurrentUser.HasAccessToEmployer(timesheet.EmployerID))
            {
                result.Data = new {Result = false};
                return result;
            }

            var parameters = new TimesheetParameters(timesheet, RepositoryAction.Create).Parameters;

            result.Data = new
            {
                Data = _repository.Create(parameters),
                Result = true
            };

            return result;
        }
    }
}
