﻿using System;
using TollFeeCalculator.Common.Extensions;
using TollFeeCalculator.Core.Services.Strategies.Interfaces;

namespace TollFeeCalculator.Core.Services.Strategies
{
    public class WorkDayTollCalculator: ITollCalculator
    {
        /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
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

            int hour = date.Hour;
            int minute = date.Minute;

            if (hour == 6 && minute >= 0 && minute <= 29) return 9;
            if (hour == 6 && minute >= 30 && minute <= 59) return 16;
            if (hour == 7 && minute >= 0 && minute <= 59) return 22;
            if (hour == 8 && minute >= 0 && minute < 29) return 16;
            if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 9;
            if (hour == 15 && minute >= 0 && minute <= 29) return 16;
            if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 22;
            if (hour == 17 && minute >= 0 && minute <= 59) return 16;
            if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            return 0;
        }
    }
}