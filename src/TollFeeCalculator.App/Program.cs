using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TollFeeCalculator.App.Services.Factories;
using TollFeeCalculator.Core.Services.Strategies;

namespace TollFeeCalculator.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var shouldExit = false;

            while (!shouldExit)
            {
                WriteLineToConsole(ConsoleColor.DarkYellow, "Welcome to the toll fee calculation tool");
                Console.WriteLine("Please select vehicle type:");
                Console.WriteLine("Type [1] for CAR");
                Console.WriteLine("Type [2] for DIPLOMAT");
                Console.WriteLine("Type [3] for EMERGENCY");
                Console.WriteLine("Type [4] for FOREIGN");
                Console.WriteLine("Type [5] for MILITARY");
                Console.WriteLine("Type [6] for MOTORBIKE");
                Console.WriteLine("Type [7] for TRACTOR");

                var vehicleTypeKeyInfo = Console.ReadKey();
                
                Console.WriteLine();
                
                var vehicleTypeStr = ((int)vehicleTypeKeyInfo.Key).ToString(CultureInfo.InvariantCulture);
                
                if (!Enum.TryParse<VehicleType>(vehicleTypeStr, out var vehicleType))
                {
                    WriteLineToConsole(ConsoleColor.Red, $"Input vehicle type {vehicleTypeStr} is not a valid vehicle type. Exiting....");
                    break;
                }

                var vehicle = VehicleFactory.Create(vehicleType);

                if (vehicle == null)
                {
                    WriteLineToConsole(ConsoleColor.Red, $"Input vehicle type {vehicleType} wasn't recognized. Exiting....");
                    break;
                }
                
                Console.WriteLine($"Selected vehicle type is: {vehicle.DisplayName}");
                WriteLineToConsole(ConsoleColor.Yellow, "Enter dates for calculating toll fee. Each date must have next format: 'yyyy-MM-dd HH:mm:ss'. In case of multiple dates, please separate them by comma sign");
                WriteLineToConsole(ConsoleColor.Yellow, "Example: 2021-05-10 00:15:00,2021-05-10 05:15:00,2021-05-10 14:47:00");

                var inputDatesStr = Console.ReadLine();

                if (string.IsNullOrEmpty(inputDatesStr))
                {
                    WriteLineToConsole(ConsoleColor.Red, "Input string of dates should not be null or empty. Exiting....");
                    break;
                }
                
                var inputDatesStrArray = inputDatesStr.Split(",");

                var dates = new List<DateTime>(inputDatesStrArray.Length);
                
                foreach (var dateStr in inputDatesStrArray)
                {
                    if (!DateTime.TryParseExact(dateStr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                    {
                        WriteLineToConsole(ConsoleColor.Red, $"Input string of dates has invalid item: {dateStr}. Exiting....");
                        break;
                    }
                    
                    dates.Add(date);
                }
                
                var tollCalculationContext = new TollFeeCalculationContext();
                
                var datesGroupedByDay = dates
                    .GroupBy(d => new {d.Day, d.Month, d.Year})
                    .ToList();

                foreach (var group in datesGroupedByDay)
                {
                    var datesArray = group.ToArray();
                
                    var resultFee = tollCalculationContext.CalculateTollFeeForSingleDay(vehicle, datesArray);

                    var groupKey = group.Key;

                    WriteLineToConsole(ConsoleColor.Green, $"Result toll fee for date: {groupKey.Year}-{groupKey.Month}-{groupKey.Day} is: {resultFee}");
                    Console.WriteLine("---------------------------------------------------------------------");
                }
                
                WriteLineToConsole(ConsoleColor.Cyan, "If you want to exit, please type [x] key. Press any other key to continue");
                
                var key = Console.ReadKey();
                var keyCode = key.Key;

                Console.WriteLine();
                
                if (keyCode == ConsoleKey.X)
                {
                    shouldExit = true;
                    WriteLineToConsole(ConsoleColor.Magenta, "Bye...");
                }
            }
        }
        
        private static void WriteLineToConsole(ConsoleColor consoleColor, string message)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}