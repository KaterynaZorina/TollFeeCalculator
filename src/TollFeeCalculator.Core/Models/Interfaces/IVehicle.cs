using TollFeeCalculator.Core.Enums;

namespace TollFeeCalculator.Core.Models.Interfaces
{
    public interface IVehicle
    {
        VehicleType GetVehicleType();

        public string DisplayName { get; }
    }
}