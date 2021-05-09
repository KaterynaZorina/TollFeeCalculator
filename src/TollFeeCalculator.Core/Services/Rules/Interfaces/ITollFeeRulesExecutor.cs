using System;

namespace TollFeeCalculator.Core.Services.Rules.Interfaces
{
    public interface ITollFeeRulesExecutor
    {
        TollFeeRulesExecutor AddRule(IRule rule);

        int CalculateFee(DateTime date);

        int RulesCount { get; }
    }
}