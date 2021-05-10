using TollFeeCalculator.Core.Enums;
using TollFeeCalculator.Core.Models.Interfaces;

namespace TollFeeCalculator.Core.Models.Vehicles
{
    public class Car: IVehicle
    {
        public VehicleType GetVehicleType()
            => VehicleType.Regular;

        public string DisplayName => "Car";
    }
}