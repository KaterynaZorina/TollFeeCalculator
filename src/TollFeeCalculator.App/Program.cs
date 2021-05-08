using System;
using TollFeeCalculator.Core.Models;
using TollFeeCalculator.Core.Services.Strategies;

namespace TollFeeCalculator.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var tollCalculationContext = new TollCalculationContext();

            var car = new Car();
            var dates = new[]
            {
                new DateTime(2021, 05, 10, 5, 15, 0),
                new DateTime(2021, 05, 10, 5, 30, 0),
                new DateTime(2021, 05, 10, 12, 00, 0),
                new DateTime(2021, 05, 10, 14, 47, 0),
                new DateTime(2021, 05, 10, 15, 18, 0),
                new DateTime(2021, 05, 10, 16, 5, 0),
                new DateTime(2021, 05, 10, 16, 15, 0),
                new DateTime(2021, 05, 10, 20, 37, 0),
                new DateTime(2021, 05, 10, 23, 00, 0),
                new DateTime(2021, 05, 10, 0, 15, 0),
            };

            var resultFee = tollCalculationContext.GetTollFee(car, dates);

            Console.WriteLine($"Result value is: {resultFee}" );
            Console.ReadLine();
        }
    }
}