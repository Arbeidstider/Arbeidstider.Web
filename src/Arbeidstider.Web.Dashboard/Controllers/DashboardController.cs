using System.Web.Mvc;

namespace Arbeidstider.Web.Controllers
{
    public class DashboardController : BaseController
    {
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
