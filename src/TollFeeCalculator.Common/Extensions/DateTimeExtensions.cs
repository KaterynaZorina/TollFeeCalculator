using System;
using PublicHoliday;

namespace TollFeeCalculator.Common.Extensions
{
    public static class DateTimeExtensions
    {
        // TODO: Add documentation
        public static bool IsDayOff(this DateTime date)
        {
            var isHoliday = new SwedenPublicHoliday().IsWorkingDay(date);

            return isHoliday;
        }
    }
}