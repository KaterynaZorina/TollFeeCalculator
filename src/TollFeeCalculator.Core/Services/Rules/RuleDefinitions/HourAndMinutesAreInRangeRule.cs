using System;
using TollFeeCalculator.Core.Services.Rules.Interfaces;

namespace TollFeeCalculator.Core.Services.Rules.RuleDefinitions
{
    public class HourAndMinutesAreInRangeRule: IRule
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