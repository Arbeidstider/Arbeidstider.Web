using System.Web.Mvc;
using Arbeidstider.Web.Filters;

namespace Arbeidstider.Web.Controllers
{
    public class DashboardController : Controller
    {
        [Authorization]
        public ActionResult Index()
        {
            return View();
        }
    }
}
