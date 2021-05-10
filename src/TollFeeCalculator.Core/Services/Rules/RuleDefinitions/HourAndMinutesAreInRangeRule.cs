using System;
using TollFeeCalculator.Core.Services.Rules.Interfaces;

namespace TollFeeCalculator.Core.Services.Rules.RuleDefinitions
{
    public readonly struct HourAndMinutesAreInRangeRule: IRule
    {
        private readonly int _hourFrom;
        
        private readonly int _hourTo;

        private readonly int _minuteFrom;

        private readonly int _minuteTo;
        
        private readonly int _tollFee;

        public HourAndMinutesAreInRangeRule(int hourFrom, int hourTo, int minuteFrom, int minuteTo, int tollFee)
        {
            _hourFrom = hourFrom;
            _hourTo = hourTo;
            _minuteFrom = minuteFrom;
            _minuteTo = minuteTo;
            _tollFee = tollFee;
        }

        /// <summary>
        /// Calculates toll fee for <paramref name="date"/> within specific hour and minutes range
        /// </summary>
        /// <param name="date">Input date to check</param>
        /// <returns>Returns calculated toll fee if <paramref name="date"/> is in rule's hour and minute range, otherwise - returns 0</returns>
        public int GetTollFeeForDate(DateTime date)
        {
            const int defaultFee = 0;
            var hour = date.Hour;
            var minute = date.Minute;

            return hour >= _hourFrom && hour <= _hourTo && minute >= _minuteFrom && minute <= _minuteTo
                ? _tollFee
                : defaultFee;
        }
    }
}