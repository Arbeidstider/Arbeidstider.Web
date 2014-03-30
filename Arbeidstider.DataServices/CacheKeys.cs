using System;

namespace Arbeidstider.Cache
{
    public class CacheKeys
    {
        public static readonly string GetEmployee = "GetEmployee";
        public static readonly string GetWeeklyTimesheet = "GetWeeklyTimesheet";
        public static readonly string GetAllWithinRange = "GetAllWithinRange";
        public static readonly string GetAllEmployees = "GetAllEmployees";
        public static readonly string UpdateEmployee = "UpdateEmployee";
        public static readonly string GetTimesheet = "GetTimesheet";

        public static string Generate(string key)
        {
            return string.Format(key, DateTime.Now.ToString());
        }
    }
}
