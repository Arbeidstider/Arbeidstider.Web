using System.Web;
using System.Web.Mvc;

namespace Arbeidstider.Web.Dashboard
{
    public static class StringHelper
    {
        public static string Fullname(this HtmlHelper helper)
        {
            return "Johan Nordström";
        }
    }
}