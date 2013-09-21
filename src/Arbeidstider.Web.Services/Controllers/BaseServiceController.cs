using System.Web.Mvc;
using Arbeidstider.Web.Services.Models;

namespace Arbeidstider.Web.Services.Controllers
{
    public class BaseServiceController : Controller
    {
        public User CurrentUser { get; set; }
    }
}