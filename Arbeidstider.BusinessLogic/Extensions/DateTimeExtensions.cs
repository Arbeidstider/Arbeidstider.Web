using System;

namespace Arbeidstider.BusinessLogic.Extensions
{
    public static class DateTimeExtensions
    {

        private static readonly DateTime UnixEpochStart =
                       DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);


        // Return to bootstrap calendar
        public static long ToMilliSeconds(this DateTime date)
        {
            var utc = date.ToUniversalTime();
            long ts = (long)((utc - UnixEpochStart).TotalMilliseconds);
            return ts;
        }

        public static DateTime ToDate(this long ms)
        {
            DateTime result = UnixEpochStart.AddMilliseconds(ms);
            return result;
        }
    }
}
