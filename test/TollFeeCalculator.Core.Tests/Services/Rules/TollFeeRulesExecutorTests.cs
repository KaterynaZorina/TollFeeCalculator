using System;
using FluentAssertions;
using NSubstitute;
using TollFeeCalculator.Core.Services.Rules;
using TollFeeCalculator.Core.Services.Rules.Interfaces;
using Xunit;

namespace TollFeeCalculator.Core.Tests.Services.Rules
{
    public class TollFeeRulesExecutorTests
    {
        // TODO: Think that implementation is a struct
        [Fact]
        public void AddRule_InputDataIsNotValid_ShouldThrowArgumentNullOrEmptyException()
        {
            // Arrange
            
            var ruleExecutor = new TollFeeRulesExecutor();
            
            // Act

            var ex = Assert.Throws<ArgumentNullException>(() => ruleExecutor.AddRule(null));

            // Assert

            ex.Should().NotBeNull();
            ex.GetType().Should().Be(typeof(ArgumentNullException));
        }

        [Fact]
        public void AddRule_InputDataIsValid_ShouldAddRuleToRulesArray()
        {
            // Arrange
            
            var ruleExecutor = new TollFeeRulesExecutor();

            var rule = Substitute.For<IRule>();

            // Act

            ruleExecutor.AddRule(rule);

            // Assert

            ruleExecutor.RulesCount.Should().Be(1);
        }

        [Fact]
        public void CalculateFee_RulesArrayIsEmpty_ShouldReturnZero()
        {
            // Arrange
            
            // Act
            
            // Assert
        }

        [Fact]
        public void CalculateFee_RulesArrayIncludesRuleForInputDate_ReturnsExpectedFee()
        {
            // Arrange
            
            // Act
            
            // Assert
        }

        [Fact]
        public void CalculateFee_RulesArrayDoesNotIncludeRuleForInputDate_ShouldReturnZero()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
    }
}