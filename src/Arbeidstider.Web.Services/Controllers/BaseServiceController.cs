using System.Web.Mvc;
using Arbeidstider.Web.Framework.ViewModels.Account;

namespace Arbeidstider.Web.Services.Controllers
{
    public class BaseServiceController : Controller
    {
        public EmployeeUser CurrentUser { get; set; }
    }
}