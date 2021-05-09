using System;

namespace TollFeeCalculator.Core.Services.Strategies.Interfaces
{
    public interface ITollCalculator
    {
        int Calculate(DateTime[] dates);
    }
}