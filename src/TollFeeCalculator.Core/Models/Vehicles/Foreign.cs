using TollFeeCalculator.Core.Enums;
using TollFeeCalculator.Core.Models.Interfaces;

namespace TollFeeCalculator.Core.Models.Vehicles
{
    public class Foreign: IVehicle
    {
        public VehicleType GetVehicleType()
            => VehicleType.TollFree;

        public string DisplayName => "Foreign";
    }
}