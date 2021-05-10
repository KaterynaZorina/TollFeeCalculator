using System;
using PublicHoliday;

namespace TollFeeCalculator.Common.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Checks whether input date is a weekend or public holiday
        /// </summary>
        /// <param name="date">Input date to check</param>
        /// <returns>Returns true if <paramref name="date"/> is a day off, otherwise - returns false</returns>
        public static bool IsDayOff(this DateTime date)
        {
            var isWorkingDay = new SwedenPublicHoliday().IsWorkingDay(date);

            return !isWorkingDay;
        }
    }
}