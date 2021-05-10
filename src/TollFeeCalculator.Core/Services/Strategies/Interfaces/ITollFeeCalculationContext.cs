using System;
using TollFeeCalculator.Core.Models.Interfaces;

namespace TollFeeCalculator.Core.Services.Strategies.Interfaces
{
    public interface ITollFeeCalculationContext
    {
        int CalculateTollFeeForSingleDay(IVehicle vehicle, DateTime[] dates);
    }
}