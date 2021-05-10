using System;
using System.Linq;
using TollFeeCalculator.Core.Models;
using TollFeeCalculator.Core.Models.Vehicles;
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
                new DateTime(2021, 05, 10, 0, 15, 0), // 0 SEK
                new DateTime(2021, 05, 10, 5, 15, 0), // 0 SEK
                new DateTime(2021, 05, 10, 5, 30, 0), // 0 SEK
                new DateTime(2021, 05, 10, 12, 00, 0), // 9 SEK
                new DateTime(2021, 05, 10, 14, 47, 0), // 9 SEK
                new DateTime(2021, 05, 10, 15, 18, 0), // 0 SEK
                new DateTime(2021, 05, 10, 16, 5, 0), // 22 SEK
                new DateTime(2021, 05, 10, 16, 15, 0), // 0 SEK
                new DateTime(2021, 05, 10, 20, 37, 0), // 0 SEK
                new DateTime(2021, 05, 10, 23, 00, 0) // 0 SEK
            };

            var datesGroupedByDay = dates
                .GroupBy(d => new {d.Day, d.Month, d.Year})
                .ToList();

            foreach (var group in datesGroupedByDay)
            {
                var datesArray = group.ToArray();
                
                var resultFee = tollCalculationContext.CalculateTollFeeForSingleDay(car, datesArray);

                var groupKey = group.Key;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Result toll fee for date: {groupKey.Year}-{groupKey.Month}-{groupKey.Day} is: {resultFee}" );
                Console.WriteLine("---------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.White;
            }
            
            Console.ReadLine();
        }
    }
}