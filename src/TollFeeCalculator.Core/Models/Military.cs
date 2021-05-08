﻿using TollFeeCalculator.Core.Enums;
using TollFeeCalculator.Core.Models.Interfaces;

namespace TollFeeCalculator.Core.Models
{
    public class Military: IVehicle
    {
        public VehicleType GetVehicleType()
            => VehicleType.TollFree;
    }
}