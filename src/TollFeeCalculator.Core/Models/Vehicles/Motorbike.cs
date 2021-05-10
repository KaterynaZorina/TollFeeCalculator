using TollFeeCalculator.Core.Enums;
using TollFeeCalculator.Core.Models.Interfaces;

namespace TollFeeCalculator.Core.Models.Vehicles
{
    public class Motorbike: IVehicle
    {
        public VehicleType GetVehicleType()
            => VehicleType.TollFree;

        public string DisplayName => "Motorbike";
    }
}