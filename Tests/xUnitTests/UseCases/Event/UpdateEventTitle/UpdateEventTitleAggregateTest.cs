﻿using DCA_VEA.Core.Domain.Aggregates.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests.UseCases.Event.UpdateEventTitle
{
    public class UpdateEventTitleAggregateTest
    {
        [Theory]
        [InlineData("Scary Movie Night!")]
        [InlineData("Graduation Gala")]
        [InlineData("VIA Hackathon")]
        public void UpdateTitle_WithValidTitle_ShouldUpdateTitle(string validTitle)
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();

            // Act
            var result = eventVEA.UpdateTitle(validTitle);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(validTitle, eventVEA.Title.Value);
        }

        [Fact]
        public void UpdateTitle_WithEmptyTitle_ReturnsFailureWithMessage()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();

            // Act
            var result = eventVEA.UpdateTitle("");

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void UpdateTitle_WithShortLengthTitle_ReturnsFailureWithMessage()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();

            // Act
            var result = eventVEA.UpdateTitle("t");

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void UpdateTitle_WithLongLengthTitle_ReturnsFailureWithMessage()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();

            // Act
            var result = eventVEA.UpdateTitle(new string('A', 76));

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
