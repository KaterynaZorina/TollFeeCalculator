using System;
using TollFeeCalculator.Core.Services.Rules.Interfaces;

namespace TollFeeCalculator.Core.Services.Rules.RuleDefinitions
{
    public readonly struct FixedHourAndMinutesAreInRangeRule: IRule
    {
        private readonly int _hour;

        private readonly int _minuteFrom;

        private readonly int _minuteTo;

        private readonly int _tollFee;

        public FixedHourAndMinutesAreInRangeRule(int hour, int minuteFrom, int minuteTo, int tollFee)
        {
            _hour = hour;
            _minuteFrom = minuteFrom;
            _minuteTo = minuteTo;
            _tollFee = tollFee;
        }
        
        public int GetTollFeeForDate(DateTime date)
        {
            const int defaultFee = 0;
            var hour = date.Hour;
            var minute = date.Minute;

            return hour == _hour && minute >= _minuteFrom && minute <= _minuteTo ? _tollFee : defaultFee;
        }
    }
}