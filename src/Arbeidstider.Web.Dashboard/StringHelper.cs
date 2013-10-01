using System.Web;
using System.Web.Mvc;
using Arbeidstider.Web.Framework.Services;

namespace Arbeidstider.Web.Dashboard
{
    public static class StringHelper
    {
        public static string Fullname(this HtmlHelper helper)
        {
            var username = HttpContext.Current.User.Identity.Name;
            var employee = EmployeeService.Instance.GetEmployee(username);
            if (employee != null)
            {
                return employee.Fullname;
            }

            return string.Empty;
        }
    }
}