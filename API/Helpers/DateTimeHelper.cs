using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime FromUnixTimestamp(long unixTimeStamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).UtcDateTime;
        }

        public static long ToUnixTimestamp(DateTime utcDateTime)
        {
            return ((DateTimeOffset)utcDateTime).ToUnixTimeSeconds();
        }

        public static bool IsValidFutureUnixTimestamp(long unixTimeStamp)
        {
            var dt = FromUnixTimestamp(unixTimeStamp);
            return dt < DateTime.UtcNow;
        }
    }
}