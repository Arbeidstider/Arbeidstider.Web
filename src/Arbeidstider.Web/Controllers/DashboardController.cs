using System.Web.Mvc;

namespace Arbeidstider.Web.Controllers
{
    public class DashboardController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View("UserProfile");
        }
    }
}
