using System;
using TollFeeCalculator.Core.Models.Interfaces;

namespace TollFeeCalculator.Core.Services.Strategies.Interfaces
{
    public interface ITollCalculationContext
    {
        int GetTollFee(IVehicle vehicle, DateTime[] dates);
    }
}