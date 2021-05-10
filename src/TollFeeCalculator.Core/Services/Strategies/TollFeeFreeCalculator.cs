using System;
using TollFeeCalculator.Core.Services.Strategies.Interfaces;

namespace TollFeeCalculator.Core.Services.Strategies
{
    public class TollFeeFreeCalculator: ITollCalculator
    {
        /// <summary>
        /// Calculates toll fee for a toll free vehicles using <paramref name="dates"/> as an input
        /// </summary>
        /// <param name="dates">Dates for calculating toll fee</param>
        /// <returns>Returns toll fee for <paramref name="dates"/></returns>
        public int Calculate(DateTime[] dates)
        {
            return 0;
        }
    }
}