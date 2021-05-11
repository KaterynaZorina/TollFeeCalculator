using System;
using FluentAssertions;
using NSubstitute;
using TollFeeCalculator.Core.Services.Rules;
using TollFeeCalculator.Core.Services.Rules.Interfaces;
using TollFeeCalculator.Core.Services.Rules.RuleDefinitions;
using Xunit;

namespace TollFeeCalculator.Core.Tests.Services.Rules
{
    public class TollFeeRulesExecutorTests
    {
        [Fact]
        public void AddRule_InputDataIsNotValid_ShouldThrowArgumentNullOrEmptyException()
        {
            // Arrange
            
            var rulesExecutor = new TollFeeRulesExecutor();
            
            // Act

            var ex = Assert.Throws<ArgumentNullException>(() => rulesExecutor.AddRule(null));

            // Assert

            ex.Should().NotBeNull();
            ex.GetType().Should().Be(typeof(ArgumentNullException));
        }

        [Fact]
        public void AddRule_InputDataIsValid_ShouldAddRuleToRulesArray()
        {
            // Arrange
            
            var rulesExecutor = new TollFeeRulesExecutor();
            var rule = Substitute.For<IRule>();

            // Act

            rulesExecutor.AddRule(rule);

            // Assert

            rulesExecutor.RulesCount.Should().Be(1);
        }

        [Fact]
        public void CalculateFee_RulesArrayIsEmpty_ShouldReturnZero()
        {
            // Arrange
            
            var rulesExecutor = new TollFeeRulesExecutor();
            var inputDate = new DateTime(2021, 05, 13);
            
            // Act

            var resultFee = rulesExecutor.CalculateFee(inputDate);

            // Assert

            rulesExecutor.RulesCount.Should().Be(0);
            resultFee.Should().Be(0);
        }

        [Fact]
        public void CalculateFee_RulesArrayIncludesRuleForInputDate_ReturnsExpectedFee()
        {
            // Arrange

            const int expectedFee = 16;

            var inputDate = new DateTime(2021, 1, 12, 14, 24, 0);
            var ruleToAdd = new FixedHourAndMinutesAreInRangeRule(14, 10, 29, expectedFee);
            var rulesExecutor = new TollFeeRulesExecutor();
            rulesExecutor.AddRule(ruleToAdd);

            // Act

            var resultFee = rulesExecutor.CalculateFee(inputDate);

            // Assert

            rulesExecutor.RulesCount.Should().Be(1);
            resultFee.Should().Be(expectedFee);
        }

        [Fact]
        public void CalculateFee_RulesArrayDoesNotIncludeRuleForInputDate_ShouldReturnZero()
        {
            // Arrange

            const int expectedFee = 0;

            var inputDate = new DateTime(2021, 3, 7, 9, 15, 0);
            var ruleToAdd = new FixedHourAndMinutesAreInRangeRule(20, 5, 45, expectedFee);
            var rulesExecutor = new TollFeeRulesExecutor();
            rulesExecutor.AddRule(ruleToAdd);
            
            // Act

            var resultFee = rulesExecutor.CalculateFee(inputDate);
            
            // Assert
            
            rulesExecutor.RulesCount.Should().Be(1);
            resultFee.Should().Be(expectedFee);
        }
    }
}