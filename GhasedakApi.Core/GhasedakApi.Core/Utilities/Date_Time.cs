using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhasedakApi.Core.Utilities
{
    public class Date_Time
    {
        public static double DatetimeToUnixTimeStamp(DateTime date, int Time_Zone = 0)
        {
            DateTime EPOCH = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan The_Date = (date - EPOCH);
            return Math.Floor(The_Date.TotalSeconds) - (Time_Zone * 3600);
        }
    }
}