using System;
using FluentAssertions;
using TollFeeCalculator.Core.Services.Rules.RuleDefinitions;
using Xunit;

namespace TollFeeCalculator.Core.Tests.Services.Rules.RuleDefinitions
{
    public class FixedHourAndMinutesAreInRangeRuleTests
    {
        [Fact]
        public void GetTollFeeForDate_InputDateSatisfiesRule_ShouldReturnExpectedTollFee()
        {
            // Arrange

            const int expectedFee = 20;
            
            var inputDate = new DateTime(2021, 5, 15, 15, 23, 0);
            var rule = new FixedHourAndMinutesAreInRangeRule(15, 0, 29, expectedFee);
            
            // Act

            var resultFee = rule.GetTollFeeForDate(inputDate);

            // Assert

            resultFee.Should().Be(expectedFee);
        }

        [Fact]
        public void GetTollFeeForDate_InputDateDoesNotSatisfyRule_ShouldReturnZero()
        {
            // Arrange

            const int expectedFee = 0;

            var inputDate = new DateTime(2020, 6, 18, 10, 18, 0);
            var rule = new FixedHourAndMinutesAreInRangeRule(14, 12, 29, expectedFee);
            
            // Act

            var resultFee = rule.GetTollFeeForDate(inputDate);
            
            // Assert
            
            resultFee.Should().Be(expectedFee);
        }
    }
}