namespace Arbeidstider.Web.Services
{
    public class CacheKeys
    {
        public static readonly string FindSchedules = "FindSchedules_{0}_{1}_{2}";
    }

    public class CacheKey
    {
        public static string Create(string key, object parameters)
        {
            return string.Format("{0}:{1}", key, parameters.GetHashCode()); 
        }
    }
}
