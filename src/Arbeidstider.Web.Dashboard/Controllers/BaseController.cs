using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Framework.ViewModels.Account;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    public class BaseController : Controller
    {
        private readonly string[] _adminActions = {"Register", "FlushCache"};
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_adminActions.Contains(filterContext.ActionDescriptor.ActionName))
                CheckAdminAccess();
        }
        protected internal Guid CurrentUserID
        {
            get
            {
                var user = Membership.GetUser(CurrentUser);
                if (user == null || user.ProviderUserKey == null) return Guid.Empty;

                return (Guid)user.ProviderUserKey;
            }
        }

        protected internal string CurrentUser
        {
            get
            {
                return HttpContext.User.Identity.Name;
            }
        }

        protected internal EmployeeUser CurrentEmployee
        {
            get
            {
                var employee = EmployeeService.Instance.GetEmployee(CurrentUser);
                if (employee != null) return employee;

                return new EmployeeUser();
            }
        }

        protected internal void CheckAdminAccess()
        {
            var employee = EmployeeService.Instance.GetEmployee(CurrentUser);
            if (employee != null && employee.IsAdmin()) return;

            RedirectToAction("Unauthorized", "Error");
        }
    }
}
