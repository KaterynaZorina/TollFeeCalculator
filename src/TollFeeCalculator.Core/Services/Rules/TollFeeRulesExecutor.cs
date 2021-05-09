using System;
using System.Collections.Generic;
using TollFeeCalculator.Core.Services.Rules.Interfaces;

namespace TollFeeCalculator.Core.Services.Rules
{
    public class TollFeeRulesExecutor: ITollFeeRulesExecutor
    {
        private readonly HashSet<IRule> _rules = new HashSet<IRule>();
        
        public int RulesCount => _rules.Count;

        // TODO: Think that implementation is a struct
        public TollFeeRulesExecutor AddRule(IRule rule)
        {
            if (rule == null)
            {
                throw new ArgumentNullException(nameof(rule));
            }

            _rules.Add(rule);

            return this;
        }

        public int CalculateFee(DateTime date)
        {
            var resultFee = 0;
            
            foreach (var rule in _rules)
            {
                var feeFromRule = rule.GetTollFeeForDate(date);

                if (feeFromRule == 0) continue;
                
                resultFee = feeFromRule;
                break;
            }

            return resultFee;
        }
    }
}