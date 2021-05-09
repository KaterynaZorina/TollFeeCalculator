using System;

namespace TollFeeCalculator.Core.Services.Rules.Interfaces
{
    public interface IRule
    {
        int GetTollFeeForDate(DateTime date);
    }
}
