﻿using System;

namespace Arbeidstider.Web.Framework.Cache
{
    public class CacheKeys
    {
        public static readonly string GetEmployee = "GetEmployee";
        public static readonly string GetWeeklyTimesheet = "GetWeeklyTimesheet";
        public static readonly string GetAllWithinRange = "GetAllWithinRange";
        public static readonly string GetAllEmployees = "GetAllEmployees";
        public static readonly string UpdateEmployee = "UpdateEmployee";
        public static readonly string GetTimesheet = "GetTimesheet";
        public static readonly string UpdateTimesheet = "UpdateTimesheet";
        public static readonly string GetAllTimesheets = "GetAllTimesheets";
        
        public static string Generate(string key)
        {
            return string.Format(key, DateTime.Now.ToString());
        }
    }
    public class CacheKey
    {
        public CacheKey()
        {
        }
        public override string ToString()
        {
        }

        public static string Create(string key, object parameters)
        {
            return string.Format("{0}:{1}", key, parameters.GetHashCode()); 
        }
    }
}
