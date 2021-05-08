using System;
using TollFeeCalculator.Core.Services.Strategies.Interfaces;

namespace TollFeeCalculator.Core.Services.Strategies
{
    public class TollFreeCalculator: ITollCalculator
    {
        public int GetTollFee(DateTime[] dates)
        {
            return 0;
        }
    }
}