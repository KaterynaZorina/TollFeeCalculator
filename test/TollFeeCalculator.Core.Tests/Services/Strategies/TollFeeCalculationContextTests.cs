using System;
using FluentAssertions;
using NSubstitute;
using TollFeeCalculator.Core.Models.Interfaces;
using TollFeeCalculator.Core.Models.Vehicles;
using TollFeeCalculator.Core.Services.Strategies;
using Xunit;

namespace TollFeeCalculator.Core.Tests.Services.Strategies
{
    public class TollFeeCalculationContextTests
    {
        [Fact]
        public void CalculateTollFeeForSingleDay_VehicleObjectIsNull_ShouldThrowArgumentNullReferenceException()
        {
            // Arrange
            
            var calculator = new TollFeeCalculationContext();
            var inputDates = Array.Empty<DateTime>();
            
            // Act

            var ex
                = Assert.Throws<ArgumentNullException>(() => calculator.CalculateTollFeeForSingleDay(null, inputDates));

            // Assert
            
            ex.Should().NotBeNull();
            ex.GetType().Should().Be(typeof(ArgumentNullException));
        }

        [Fact]
        public void CalculateTollFeeForSingleDay_ArrayOfDatesIsNull_ShouldThrowArgumentNullReferenceException()
        {
            // Arrange
            
            var calculator = new TollFeeCalculationContext();
            var vehicle = Substitute.For<IVehicle>();

            // Act

            var ex
                = Assert.Throws<ArgumentNullException>(() => calculator.CalculateTollFeeForSingleDay(vehicle, null));
            
            // Assert
            
            ex.Should().NotBeNull();
            ex.GetType().Should().Be(typeof(ArgumentNullException));
        }

        [Fact]
        public void CalculateTollFeeForSingleDay_ArrayOfDatesContainsNotAllowedDates_ShouldThrowArgumentException()
        {
            // Arrange
            
            var calculator = new TollFeeCalculationContext();
            var vehicle = Substitute.For<IVehicle>();
            var inputDates = new[]
            {
                new DateTime(2021, 10, 9, 10, 15, 0),
                new DateTime(2021, 10, 9, 13, 5, 12),
                new DateTime(2021, 7, 10, 14, 22, 0)
            };

            // Act

            var ex
                = Assert.Throws<ArgumentException>(() => calculator.CalculateTollFeeForSingleDay(vehicle, inputDates));
            
            // Assert
            
            const string errorMessage = "dates array must contain dates with the same year, month and day";
            
            ex.Should().NotBeNull();
            ex.GetType().Should().Be(typeof(ArgumentException));
            ex.Message.Should().Be(errorMessage);
        }
        
        [Fact]
        public void CalculateTollFeeForSingleDay_ArrayOfDatesIsEmpty_ShouldReturnZero()
        {
            // Arrange
            
            const int expectedFee = 0;
            
            var calculator = new TollFeeCalculationContext();
            var vehicle = Substitute.For<IVehicle>();
            
            // Act

            var resultFee = calculator.CalculateTollFeeForSingleDay(vehicle, Array.Empty<DateTime>());

            // Assert

            resultFee.Should().Be(expectedFee);
        }

        [Theory]
        [InlineData(6, 0, 9)]
        [InlineData(6, 35, 16)]
        [InlineData(7, 15, 22)]
        [InlineData(8, 18, 16)]
        [InlineData(8, 36, 9)]
        [InlineData(15, 15, 16)]
        [InlineData(15, 35, 22)]
        [InlineData(16, 5, 22)]
        [InlineData(17, 40, 16)]
        [InlineData(18, 20, 9)]
        [InlineData(19, 20, 0)]
        [InlineData(22, 18, 0)]
        [InlineData(3, 10, 0)]
        [InlineData(5, 17, 0)]
        public void CalculateTollFeeForSingleDay_ArrayOfDatesContainsSingleDate_ShouldCalculateAndReturnFee(int hour, int minute, int expectedFee)
        {
            // Arrange
            
            const int year = 2021;
            const int month = 5;
            const int day = 10;
            
            var calculator = new TollFeeCalculationContext();
            var vehicle = new Car();
            var inputDates = new[] {new DateTime(year, month, day, hour, minute, 0)};

            // Act

            var resultFee = calculator.CalculateTollFeeForSingleDay(vehicle, inputDates);

            // Assert

            resultFee.Should().Be(expectedFee);
        }
        
        [Fact]
        public void CalculateTollFeeForSingleDay_InputDatesAreWeekendDates_ShouldReturnZero()
        {
            // Arrange
            
            var calculator = new TollFeeCalculationContext();
            var vehicle = new Car();
            var inputDates = new[] {new DateTime(2021, 5, 9, 8, 0, 0)};

            // Act

            var resultFee = calculator.CalculateTollFeeForSingleDay(vehicle, inputDates);
            
            // Assert
            
            resultFee.Should().Be(0);
        }

        [Fact]
        public void CalculateTollFeeForSingleDay_InputDatesAreHolidayDates_ShouldReturnZero()
        {
            // Arrange
            
            var calculator = new TollFeeCalculationContext();
            var vehicle = new Car();
            var inputDates = new[] {new DateTime(2021, 4, 4, 12, 0, 0)};

            // Act

            var resultFee = calculator.CalculateTollFeeForSingleDay(vehicle, inputDates);
            
            // Assert
            
            resultFee.Should().Be(0);
        }

        [Fact]
        public void CalculateTollFeeForSingleDay_InputVehicleIsTollFree_ShouldReturnZero()
        {
            // Arrange
            
            var calculator = new TollFeeCalculationContext();
            var vehicle = new Emergency();
            var inputDates = new[] {new DateTime(2021, 1, 6, 16, 5, 0)};

            // Act

            var resultFee = calculator.CalculateTollFeeForSingleDay(vehicle, inputDates);
            
            // Assert
            
            resultFee.Should().Be(0);
        }

        [Fact]
        public void CalculateTollFeeForSingleDay_ResultFeeForInputDatesExceedsMaximumFee_ShouldReturnMaximumFee()
        {
            // Arrange
            
            const int year = 2021;
            const int month = 5;
            const int day = 10;

            var calculator = new TollFeeCalculationContext();
            var vehicle = new Car();
            var inputDates = new[]
            {
                new DateTime(year, month, day, 6, 0, 0),
                new DateTime(year, month, day, 6, 35, 0),
                new DateTime(year, month, day, 7, 15, 0),
                new DateTime(year, month, day, 8, 18, 0),
                new DateTime(year, month, day, 15, 35, 0),
                new DateTime(year, month, day, 16, 5, 0),
                new DateTime(year, month, day, 17, 40, 0),
            };
            
            // Act
            
            var resultFee = calculator.CalculateTollFeeForSingleDay(vehicle, inputDates);
            
            // Assert
            
            resultFee.Should().Be(60);
        }

        [Fact]
        public void CalculateTollFeeForSingleDay_InputDatesArrayContainsMultipleDatesForTheSameHour_ShouldCalculateFeeOncePerHour()
        {
            // Arrange
            
            const int year = 2021;
            const int month = 5;
            const int day = 10;

            var calculator = new TollFeeCalculationContext();
            var vehicle = new Car();
            var inputDates = new[]
            {
                new DateTime(year, month, day, 6, 0, 12, 34), // 9 SEK
                new DateTime(year, month, day, 6, 7, 45, 15), // 0 SEK
                new DateTime(year, month, day, 7, 5, 0), // 22 SEK
                new DateTime(year, month, day, 7, 25, 18, 9), // 0 SEK
                new DateTime(year, month, day, 7, 42, 9, 15), // 0 SEK
                new DateTime(year, month, day, 15, 45, 15, 16), // 22 SEK
                new DateTime(year, month, day, 16, 0, 20, 15), // 0 SEK
            };
            
            // Act
            
            var resultFee = calculator.CalculateTollFeeForSingleDay(vehicle, inputDates);
            
            // Assert
            
            resultFee.Should().Be(53);
        }
    }
}