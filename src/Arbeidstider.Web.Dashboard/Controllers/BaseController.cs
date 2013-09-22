using System.Web;
using System.Web.Mvc;

namespace Arbeidstider.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly HttpContextBase _context;

        public BaseController()
        {
            _context = HttpContext;
        }
    }
}
