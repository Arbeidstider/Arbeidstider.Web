using System;

namespace Arbeidstider.Web.Framework.Scaffolding
{
    public static class ScaffoldHelper
    {
        public static DateTime[] GetDates(DateTime monday)
        {
            var dates = new DateTime[]
            {
                monday,
                new DateTime(monday.Year, monday.Month, monday.Day+1),
                new DateTime(monday.Year, monday.Month, monday.Day+2),
                new DateTime(monday.Year, monday.Month, monday.Day+3),
                new DateTime(monday.Year, monday.Month, monday.Day+4)
            };

            return dates;
        }

        public static TimeSpan[] GetTimes(TimeSpan startTime, TimeSpan starttime2)
        {
            var times = new TimeSpan[]
            {
                startTime,
                new TimeSpan(startTime.Hours + 8, 0, 0),
                starttime2,
                new TimeSpan(starttime2.Hours + 8, 0, 0)
            };

            return times;
        }
    }
}