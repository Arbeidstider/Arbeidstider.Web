using System.Web;

namespace Arbeidstider.Web.Constants
{
    internal class Urls
    {
        public static readonly string START_PAGE = "http://mine.arbeidstider.no:{0}".Format();
        public static readonly string USER_PROFILE = "http://mine.arbeidstider.no:{0}/profile".Format();
        public static readonly string LOGIN = "http://mine.arbeidstider.no:{0}/login".Format();
        public static readonly string LOGOFF = "http://mine.arbeidstider.no:{0}/logoff".Format();
        public static readonly string DASH_BOARD = "http://mine.arbeidstider.no:{0}/index".Format();
    }

    internal static class UrlHelper
    {
        private static readonly string _port = HttpContext.Current.IsDebuggingEnabled ? "81" : "80";
        internal static string Format(this string url)
        {
            return string.Format(url, _port);
        }
    }
}