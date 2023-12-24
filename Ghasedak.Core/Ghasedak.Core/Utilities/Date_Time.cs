using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Ghasedak.Core.Utilities
{
    public class Date_Time
    {
        public static double DatetimeToUnixTimeStamp(DateTime date, int Time_Zone = 0)
        {
            try
            {
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
                TimeSpan unixTimeSpan = (date - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local).ToLocalTime());
                return long.Parse(unixTimeSpan.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}