using TollFeeCalculator.Core.Enums;
using TollFeeCalculator.Core.Models.Interfaces;

namespace TollFeeCalculator.Core.Models
{
    public class Car: IVehicle
    {
        public VehicleType GetVehicleType()
            => VehicleType.Regular;
    }
}