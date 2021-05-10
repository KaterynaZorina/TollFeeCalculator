using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.Core.Enums;
using TollFeeCalculator.Core.Models.Interfaces;
using TollFeeCalculator.Core.Services.Strategies.Interfaces;

namespace TollFeeCalculator.Core.Services.Strategies
{
    public class TollFeeCalculationContext: ITollFeeCalculationContext
    {
        private readonly ConcurrentDictionary<VehicleType, ITollCalculator> _tollCalculators;

        public TollFeeCalculationContext()
        {
            var internalDictionary = new Dictionary<VehicleType, ITollCalculator>
            {
                {VehicleType.Regular, new RegularTollFeeCalculator()},
                {VehicleType.TollFree, new TollFeeFreeCalculator()}
            };
            
            _tollCalculators = new ConcurrentDictionary<VehicleType, ITollCalculator>(internalDictionary);
        }

        /// <summary>
        /// Calculates toll fee for <paramref name="vehicle"/> using <paramref name="dates"/>
        /// </summary>
        /// <param name="vehicle">A vehicle which was charged with toll fee</param>
        /// <param name="dates">An array of dates for calculating toll fee</param>
        /// <returns>Calculated toll fee</returns>
        /// <exception cref="ArgumentNullException">Throws an error in case if <paramref name="vehicle"/> or <paramref name="dates"/> are null</exception>
        /// <exception cref="ArgumentException">Throws an exception in case if <paramref name="dates"/> contain dates with different year, month or day</exception>
        /// <exception cref="InvalidOperationException">Throws an exception in case if <paramref name="vehicle"/> has type, which can't be handled</exception>
        public int CalculateTollFeeForSingleDay(IVehicle vehicle, DateTime[] dates)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            if (dates == null)
            {
                throw new ArgumentNullException(nameof(dates));
            }

            var groupedDates = dates.GroupBy(d => new {d.Day, d.Month, d.Year}).ToList();
            if (groupedDates.Count > 1)
            {
                throw new ArgumentException($"{nameof(dates)} array must contain dates with the same year, month and day");
            }

            const int noFeeAmount = 0;
            
            if (dates.Length == 0)
            {
                return noFeeAmount;
            }
            
            var vehicleType = vehicle.GetVehicleType();
            var calculatorExists = _tollCalculators.TryGetValue(vehicleType, out var tollCalculator);
            if (!calculatorExists)
            {
                throw new InvalidOperationException($"Received type of vehicle: {vehicleType} can't be handled");
            }

            var resultFee = tollCalculator.Calculate(dates);

            return resultFee;
        }
    }
}