using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.Core.Enums;
using TollFeeCalculator.Core.Models.Interfaces;
using TollFeeCalculator.Core.Services.Strategies.Interfaces;

namespace TollFeeCalculator.Core.Services.Strategies
{
    public class TollCalculationContext: ITollCalculationContext
    {
        private readonly ConcurrentDictionary<VehicleType, ITollCalculator> _tollCalculators;

        public TollCalculationContext()
        {
            var internalDictionary = new Dictionary<VehicleType, ITollCalculator>
            {
                {VehicleType.Regular, new WorkDayTollCalculator()},
                {VehicleType.TollFree, new TollFreeCalculator()}
            };
            
            _tollCalculators = new ConcurrentDictionary<VehicleType, ITollCalculator>(internalDictionary);
        }

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