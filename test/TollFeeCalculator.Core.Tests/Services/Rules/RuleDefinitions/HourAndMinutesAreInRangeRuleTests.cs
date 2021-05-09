using System;
using FluentAssertions;
using TollFeeCalculator.Core.Services.Rules.RuleDefinitions;
using Xunit;

namespace TollFeeCalculator.Core.Tests.Services.Rules.RuleDefinitions
{
    public class HourAndMinutesAreInRangeRuleTests
    {
        [Fact]
        public void GetTollFeeForDate_InputDateSatisfiesRule_ShouldReturnExpectedTollFee()
        {
           // Arrange
           
           const int expectedFee = 20;

           var inputDate = new DateTime(2021, 6, 15, 15, 23, 0);

           var rule = new HourAndMinutesAreInRangeRule(12, 16, 15, 25, expectedFee);
           
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
            
            var inputDate = new DateTime(2021, 7, 14, 9, 18, 0);
            
            var rule = new HourAndMinutesAreInRangeRule(12, 13, 15, 25, expectedFee);
            
            // Act
            
            var resultFee = rule.GetTollFeeForDate(inputDate);
            
            // Assert
            
            resultFee.Should().Be(expectedFee);
        }
    }
}