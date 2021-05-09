using TollFeeCalculator.Core.Enums;
using TollFeeCalculator.Core.Models.Interfaces;

namespace TollFeeCalculator.Core.Models.Vehicles
{
    public class Tractor: IVehicle
    {
        public VehicleType GetVehicleType()
            => VehicleType.TollFree;
    }
}