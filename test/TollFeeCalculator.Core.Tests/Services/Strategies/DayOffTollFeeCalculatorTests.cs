using System;
using FluentAssertions;
using TollFeeCalculator.Core.Services.Strategies;
using Xunit;

namespace TollFeeCalculator.Core.Tests.Services.Strategies
{
    public class DayOffTollFeeCalculatorTests
    {
        [Fact]
        public void Calculate_InputDatesArrayContainsSingleDate_ShouldReturnZeroForAnyInput()
        {
            // Arrange

            const int resultFee = 0;
            
            var inputDates = new[] {new DateTime(2021, 1, 12, 14, 24, 0)};
            var calculator = new DayOffTollFeeCalculator();

            // Act

            var calculatedFee = calculator.Calculate(inputDates);

            // Assert

            calculatedFee.Should().Be(resultFee);
        }
    }
}