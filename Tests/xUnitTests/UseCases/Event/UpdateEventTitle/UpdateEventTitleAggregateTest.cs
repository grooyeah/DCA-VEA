using DCA_VEA.Core.Domain.Aggregates.Events;
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
            eventVEA.UpdateTitle(validTitle);

            // Assert
            Assert.Equal(validTitle, eventVEA.Title.Value);
        }

        [Fact]
        public void UpdateTitle_WithEmptyTitle_ShouldThrowException()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => eventVEA.UpdateTitle(""));
            Assert.Equal("Title cannot be null or empty.", exception.Message);
        }

        [Fact]
        public void UpdateTitle_WithShortLengthTitle_ShouldThrowException()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => eventVEA.UpdateTitle("t"));
            Assert.Equal("Title must be between 3 and 75 characters long.", exception.Message);
        }

        [Fact]
        public void UpdateTitle_WithLongLengthTitle_ShouldThrowException()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => eventVEA.UpdateTitle("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\r\n"));
            Assert.Equal("Title must be between 3 and 75 characters long.", exception.Message);
        }
    }
}
