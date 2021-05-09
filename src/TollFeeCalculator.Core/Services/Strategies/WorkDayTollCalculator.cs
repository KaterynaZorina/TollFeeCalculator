using System;
using TollFeeCalculator.Common.Extensions;
using TollFeeCalculator.Core.Services.Rules;
using TollFeeCalculator.Core.Services.Rules.RuleDefinitions;
using TollFeeCalculator.Core.Services.Strategies.Interfaces;

namespace TollFeeCalculator.Core.Services.Strategies
{
    public class WorkDayTollCalculator: ITollCalculator
    {
        public int GetTollFee(DateTime[] dates)
        {
            DateTime intervalStart = dates[0];
            int totalFee = 0;
            
            foreach (DateTime date in dates)
            {
                int tempFee = GetTollFee(intervalStart);
                int nextFee = GetTollFee(date);

                long diffInMillies = date.Millisecond - intervalStart.Millisecond;
                long minutes = diffInMillies / 1000 / 60;

                if (minutes <= 60)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }
            }

            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }

        private int GetTollFee(DateTime date)
        {
            if (date.IsDayOff()) return 0;

            var rulesExecutor = new TollFeeRulesExecutor();
            rulesExecutor.AddRule(new FixedHourAndMinutesAreInRangeRule(6, 0, 29, 9))
                .AddRule(new FixedHourAndMinutesAreInRangeRule(6, 30, 59, 16))
                .AddRule(new FixedHourAndMinutesAreInRangeRule(7, 0, 59, 22))
                .AddRule(new FixedHourAndMinutesAreInRangeRule(8, 0, 29, 16))
                .AddRule(new HourAndMinutesAreInRangeRule(8, 4, 30, 59, 9))
                .AddRule(new FixedHourAndMinutesAreInRangeRule(15, 0, 29, 16))
                .AddRule(new FixedHourAndMinutesAreInRangeRule(15, 30, 59, 22))
                .AddRule(new FixedHourAndMinutesAreInRangeRule(16, 0, 59, 22))
                .AddRule(new FixedHourAndMinutesAreInRangeRule(17, 0, 59, 16))
                .AddRule(new FixedHourAndMinutesAreInRangeRule(18, 0, 29, 8));

            var resultTollFee = rulesExecutor.CalculateFee(date);

            return resultTollFee;
        }
    }
}