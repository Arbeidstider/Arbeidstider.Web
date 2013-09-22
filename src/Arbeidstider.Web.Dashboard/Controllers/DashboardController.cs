using System.Web.Mvc;
using Arbeidstider.Business.Interfaces.Services;
using Arbeidstider.Web.Dashboard.Filters;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    [Authorization]
    public class DashboardController : BaseController
    {
        public DashboardController(IUserService userservice) : base(userservice)
        {
            
        }
        public ActionResult Index()
        {
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
