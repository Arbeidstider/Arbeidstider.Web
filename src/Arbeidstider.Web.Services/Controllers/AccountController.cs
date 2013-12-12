using System.Web.Mvc;
using System.Web.Security;

namespace Arbeidstider.Web.Services.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public JsonResult Login(string username, string password)
        {
            if (Membership.ValidateUser(username, password))
                return Json(true);
            return Json(false);
        }

        //
    }
}
