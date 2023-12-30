using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Ghasedak.Core.Utilities
{
    public class Date_Time
    {
        public static double DatetimeToUnixTimeStamp(DateTime date)
        {
            try
            {
                TimeZoneInfo iranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
                DateTimeOffset inputDateTimeOffset = new DateTimeOffset(date);
                DateTimeOffset iranDateTimeOffset = TimeZoneInfo.ConvertTime(inputDateTimeOffset, iranTimeZone);
                long unixTime = iranDateTimeOffset.ToUnixTimeSeconds();
                return unixTime;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}