using System;
using System.Collections.Generic;
using TollFeeCalculator.Core.Services.Rules.Interfaces;

namespace TollFeeCalculator.Core.Services.Rules
{
    public class TollFeeRulesExecutor: ITollFeeRulesExecutor
    {
        private readonly HashSet<IRule> _rules = new HashSet<IRule>();
        
        /// <summary>
        /// Rules count, defined in current executor instance
        /// </summary>
        public int RulesCount => _rules.Count;
        
        /// <summary>
        /// Adds a <paramref name="rule"/> into collection of internal rules
        /// </summary>
        /// <param name="rule">Rule to add for later processing by current executor instance</param>
        /// <returns>Returns current executor instance</returns>
        /// <exception cref="ArgumentNullException">Throws exception if <paramref name="rule"/> is null</exception>
        public TollFeeRulesExecutor AddRule(IRule rule)
        {
            if (rule == null)
            {
                throw new ArgumentNullException(nameof(rule));
            }

            _rules.Add(rule);

            return this;
        }

        /// <summary>
        /// Calculates toll fee for <paramref name="date"/> using a set of defined rules
        /// </summary>
        /// <param name="date">Date for calculating toll fee</param>
        /// <returns>Returns toll fee for <paramref name="date"/></returns>
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