namespace Arbeidstider.Web.Services.Helpers
{
    public static class StringHelper
    {
        public static string WithParameters(this string str, object param1, object param2, object param3)
        {
            return string.Format(str, param1, param2, param3);
        }
    }
}