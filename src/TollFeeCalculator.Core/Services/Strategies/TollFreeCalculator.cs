using System;
using TollFeeCalculator.Core.Services.Strategies.Interfaces;

namespace TollFeeCalculator.Core.Services.Strategies
{
    public class TollFreeCalculator: ITollCalculator
    {
        public int Calculate(DateTime[] dates)
        {
            return 0;
        }
    }
}