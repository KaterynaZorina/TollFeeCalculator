using System;
using TollFeeCalculator.Core.Services.Strategies.Interfaces;

namespace TollFeeCalculator.Core.Services.Strategies
{
    public class DayOffTollFeeCalculator: ITollCalculator
    {
        public int Calculate(DateTime[] dates)
        {
            return 0;
        }
    }
}