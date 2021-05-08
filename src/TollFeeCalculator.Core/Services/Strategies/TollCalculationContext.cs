using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

        public int GetTollFee(IVehicle vehicle, DateTime[] dates)
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
            
            var vehicleType = vehicle.GetVehicleType();
            var calculatorExists = _tollCalculators.TryGetValue(vehicleType, out var tollCalculator);

            if (!calculatorExists)
            {
                // TODO: Think about this case
                return noFeeAmount;
            }

            var resultFee = tollCalculator.GetTollFee(dates);

            return resultFee;
        }
    }
}