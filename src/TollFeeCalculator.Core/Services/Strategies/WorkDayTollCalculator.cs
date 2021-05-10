﻿using System;
using System.Linq;
using TollFeeCalculator.Common.Extensions;
using TollFeeCalculator.Core.Services.Rules;
using TollFeeCalculator.Core.Services.Rules.RuleDefinitions;
using TollFeeCalculator.Core.Services.Strategies.Interfaces;

namespace TollFeeCalculator.Core.Services.Strategies
{
    public class WorkDayTollCalculator: ITollCalculator
    {
        public int Calculate(DateTime[] dates)
        {
            const int maximumFee = 60;
            const int millisecondsInSec = 1000;
            const int secondsInMin = 60;
            const int minutesInHour = 60;

            var orderedDates = dates.OrderBy(d => d).ToList();
            var lastChargeDate = orderedDates.FirstOrDefault();
            var totalFee = 0;
            
            foreach (var chargeDate in orderedDates)
            {
                var previousFee = GetTollFee(lastChargeDate);
                var nextFee = GetTollFee(chargeDate);

                var intervalBetweenChargesInMs = (chargeDate - lastChargeDate).TotalMilliseconds;
                var intervalBetweenChargesInMins = intervalBetweenChargesInMs / millisecondsInSec / secondsInMin;

                if (intervalBetweenChargesInMins <= minutesInHour)
                {
                    if (totalFee > 0) totalFee -= previousFee;
                    if (nextFee >= previousFee) previousFee = nextFee;
                    totalFee += previousFee;
                }
                else
                {
                    totalFee += nextFee;
                    lastChargeDate = chargeDate;
                }
            }

            if (totalFee > maximumFee) totalFee = maximumFee;
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
                .AddRule(new HourAndMinutesAreInRangeRule(8, 14, 30, 59, 9))
                .AddRule(new FixedHourAndMinutesAreInRangeRule(15, 0, 29, 16))
                .AddRule(new FixedHourAndMinutesAreInRangeRule(15, 30, 59, 22))
                .AddRule(new FixedHourAndMinutesAreInRangeRule(16, 0, 59, 22))
                .AddRule(new FixedHourAndMinutesAreInRangeRule(17, 0, 59, 16))
                .AddRule(new FixedHourAndMinutesAreInRangeRule(18, 0, 29, 9));

            var resultTollFee = rulesExecutor.CalculateFee(date);

            return resultTollFee;
        }
    }
}