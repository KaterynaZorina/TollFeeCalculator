using System;
using FluentAssertions;
using TollFeeCalculator.Core.Services.Strategies;
using Xunit;

namespace TollFeeCalculator.Core.Tests.Services.Strategies
{
    public class RegularTollFeeCalculatorTests
    {
        [Fact]
        public void Calculate_InputArrayIsValid_ShouldCalculateFeeAndReturnFiftyThree()
        {
            // Arrange
            
            const int year = 2021;
            const int month = 5;
            const int day = 10;

            var calculator = new RegularTollFeeCalculator();
            var inputDates = new[]
            {
                new DateTime(year, month, day, 6, 1, 12, 34), // 9 SEK
                new DateTime(year, month, day, 6, 12, 45, 15), // 0 SEK
                new DateTime(year, month, day, 7, 2, 0), // 22 SEK
                new DateTime(year, month, day, 7, 25, 18, 9), // 0 SEK
                new DateTime(year, month, day, 7, 42, 9, 15), // 0 SEK
                new DateTime(year, month, day, 15, 55, 15, 16), // 22 SEK
                new DateTime(year, month, day, 16, 0, 20, 15), // 0 SEK
            };
            
            // Act

            var calculatedFee = calculator.Calculate(inputDates);

            // Assert
            
            calculatedFee.Should().Be(53);
        }
    }
}