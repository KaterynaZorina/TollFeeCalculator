using System;
using FluentAssertions;
using TollFeeCalculator.Common.Extensions;
using Xunit;

namespace TollFeeCalculator.Common.Tests.Extensions
{
    public class DateTimeExtensionsTests
    {
        [Theory]
        [InlineData(2020, 05, 07)]
        [InlineData(2021, 01, 11)]
        [InlineData(2018, 04, 04)]
        [InlineData(2015, 06, 08)]
        [InlineData(2015, 10, 22)]
        [InlineData(2013, 11, 11)]
        [InlineData(2013, 12, 05)]
        public void IsDayOff_InputDateIsWorkDay_ShouldReturnFalse(int year, int month, int day)
        {
            // Arrange
            
            var inputDate = new DateTime(year, month, day);
            
            // Act

            var isDayOff = inputDate.IsDayOff();

            // Assert

            isDayOff.Should().BeFalse();
        }
    }
}