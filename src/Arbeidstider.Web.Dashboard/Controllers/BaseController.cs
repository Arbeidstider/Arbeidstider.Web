using System.Web;
using System.Web.Mvc;
using Arbeidstider.Business.Interfaces.Services;
using Arbeidstider.Web.Dashboard.Helpers;
using Arbeidstider.Web.Services.Models;
using Arbeidstider.Web.Services.Parameters;
using Autofac;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    public class BaseController : Controller
    {
        private readonly HttpContextBase _context;
        private readonly IUserService _userservice;

        public BaseController(IUserService userservice)
        {
            _context = HttpContext;
            _userservice = userservice;
        }

    }
}
