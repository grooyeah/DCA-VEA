using DCA_VEA.Core.Domain.Aggregates.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests.UseCases
{
    public class UpdateTitleOfExistingEvent
    {
        // S1
        [Fact]
        public void updateTitle_ValidTitleLengthInDraftStatus_TitleUpdated()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();
            var newTitle = "Scary Movie Night!";

            // Act
            eventVEA.UpdateTitle(newTitle);

            // Assert
            var titleField = eventVEA.GetType().GetField("Title", BindingFlags.NonPublic | BindingFlags.Instance);
            var titleValue = (string)titleField.GetValue(eventVEA);
            Assert.Equal(newTitle, titleValue);
        }

        // S2
        [Fact]
        public void updateTitle_WithValidTitleLengthInReadyStatus_TitleIsUpdatedAndStatusChangesToDraft()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();
            var newTitle = "Graduation Gala";

            // Act
            eventVEA.UpdateTitle(newTitle);

            // Assert
            var titleField = eventVEA.GetType().GetField("Title", BindingFlags.NonPublic | BindingFlags.Instance);
            var titleValue = (string)titleField.GetValue(eventVEA);
            Assert.Equal(newTitle, titleValue);

            var statusField = eventVEA.GetType().GetField("Status", BindingFlags.NonPublic | BindingFlags.Instance);
            var statusValue = (EventVEAStatus)statusField.GetValue(eventVEA);
            Assert.Equal(EventVEAStatus.Draft, statusValue);
        }

        // F1 
        [Fact]
        public void updateTitle_TitleIsZeroCharacters_ReturnsFailureMessage()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();
            var title = "";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => eventVEA.UpdateTitle(title));
            Assert.Equal("Title must be between 3 and 75 characters long.", exception.Message);
        }

        // F2 
        [Theory]
        [InlineData("XY")]
        [InlineData("a")]
        public void updateTitle_TitleIsLessThanThreeCharacters_ReturnsFailureMessage(string title)
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => eventVEA.UpdateTitle(title));
            Assert.Equal("Title must be between 3 and 75 characters long.", exception.Message);
        }

        // F3
        [Fact]
        public void updateTitle_TitleIsMoreThanSeventyFiveCharacters_ReturnsFailureMessage()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();
            var title = new string('X', 76);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => eventVEA.UpdateTitle(title));
            Assert.Equal("Title must be between 3 and 75 characters long.", exception.Message);
        }

        // F4 
        [Fact]
        public void updateTitle_TitleIsNull_ReturnsFailureMessage()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => eventVEA.UpdateTitle(null));
            Assert.Equal("Title must be between 3 and 75 characters long.", exception.Message);
        }

        // F5 
        [Fact]
        public void updateTitle_EventIsInActiveStatus_ReturnsFailureMessage()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();
            typeof(EventVEA).GetField("Status", BindingFlags.NonPublic | BindingFlags.Instance)
                            .SetValue(eventVEA, EventVEAStatus.Active);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => eventVEA.UpdateTitle("New Title"));
            Assert.Equal("An active event cannot be modified.", exception.Message);
        }

        // F6
        [Fact]
        public void updateTitle_EventIsInCancelledStatus_ReturnsFailureMessage()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();
            typeof(EventVEA).GetField("Status", BindingFlags.NonPublic | BindingFlags.Instance)
                            .SetValue(eventVEA, EventVEAStatus.Cancelled);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => eventVEA.UpdateTitle("New Title"));
            Assert.Equal("A cancelled event cannot be modified.", exception.Message);
        }
    }
}
