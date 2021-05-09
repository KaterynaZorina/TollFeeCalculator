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

            const int noFeeAmount = 0;
            
            if (dates.Length == 0)
            {
                return noFeeAmount;
            }

            var groupedDates = dates.GroupBy(d => new {d.Day, d.Month, d.Year}).ToList();
            if (groupedDates.Count > 1)
            {
                throw new ArgumentException($"{nameof(dates)} array must ");
            }
            
            var vehicleType = vehicle.GetVehicleType();
            var calculatorExists = _tollCalculators.TryGetValue(vehicleType, out var tollCalculator);

            if (!calculatorExists)
            {
                // TODO: Think about this case
                return noFeeAmount;
            }

            var resultFee = tollCalculator.Calculate(dates);

            return resultFee;
        }
    }
}