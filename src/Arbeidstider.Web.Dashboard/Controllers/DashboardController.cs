using System;
using System.Web.Mvc;
using Arbeidstider.Web.Dashboard.Filters;
using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Framework.ViewModels.Dashboard;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    [Authorization]
    public class DashboardController : BaseController
    {
        private readonly TimesheetService _timesheetService;

        public DashboardController() : base()
        {
            _timesheetService = TimesheetService.Instance;

        }

        public ActionResult Index()
        {
            var model = new Index();
            model.CurrentWeeklyWorkHours = _timesheetService.GetWeeklyTimesheet(GetCurrentEmployeeID, new DateTime(2013, 9, 23));
            return View();
        }

        public ActionResult AddressBook()
        {
            return View();
        }

        public ActionResult ConfirmShifts()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            return View();
        }
    }
}
