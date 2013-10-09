using System;
using System.Web.Mvc;
using System.Web.Security;
using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Framework.ViewModels.Account;
using log4net;

namespace Arbeidstider.Web.Framework.Controllers
{
    public class BaseController : Controller
    {
        protected readonly static ILog Logger = IoC.Resolve<ILog>();
        protected static readonly EmployeeService EmployeeService = EmployeeService.Instance;
        protected static readonly TimesheetService TimesheetService = TimesheetService.Instance;

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

        protected internal IEmployeeUser CurrentEmployee
        {
            get
            {
                var employee = EmployeeService.GetEmployee(CurrentUser);
                if (employee != null) return employee;

                return null;
            }
        }

        protected internal int CurrentWorkplaceID
        {
            get
            {
                return CurrentEmployee.WorkplaceID;
            }
        }

    }
}