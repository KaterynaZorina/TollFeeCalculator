using TollFeeCalculator.Core.Models.Interfaces;
using TollFeeCalculator.Core.Models.Vehicles;

namespace TollFeeCalculator.App.Services.Factories
{
    public static class VehicleFactory
    {
        public static IVehicle Create(VehicleType vehicleType)
        {
            IVehicle vehicle = vehicleType switch
            {
                VehicleType.Car => new Car(),
                VehicleType.Diplomat => new Diplomat(),
                VehicleType.Emergency => new Emergency(),
                VehicleType.Foreign => new Foreign(),
                VehicleType.Military => new Military(),
                VehicleType.Motorbike => new Motorbike(),
                VehicleType.Tractor => new Tractor(),
                _ => null
            };

            return vehicle;
        }
    }
}