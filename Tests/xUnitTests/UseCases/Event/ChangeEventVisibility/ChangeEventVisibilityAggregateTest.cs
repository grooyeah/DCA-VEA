using DCA_VEA.Core.Domain.Aggregates.Event;
using DCA_VEA.Core.Domain.Aggregates.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests.UseCases.Event.ChangeEventVisibility
{
    public class ChangeEventVisibilityAggregateTest
    {
        [Fact]
        public void MakeEventPublic_MakesPublicAndKeepsStatus()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();
            var initialStatus = eventVEA.Status;

            // Act
            var result = eventVEA.MakePublic();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(EventTypes.Public, eventVEA.EventType.Value);
            Assert.Equal(initialStatus.Value, eventVEA.Status.Value);
        }

        [Fact]
        public void MakeEventPublic_WhenEventIsCancelled_ReturnsFailure()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();
            eventVEA.Cancel(); // Updated to use the method that now sets status correctly and returns Result<bool>

            // Act
            var result = eventVEA.MakePublic();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(result.Errors, e => e.Message == "A cancelled event cannot be modified.");
        }


        [Fact]
        public void MakeEventPrivate_MakesPrivateAndUpdatesStatus()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();

            // Act
            var result = eventVEA.MakePrivate();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(EventTypes.Private, eventVEA.EventType.Value);
            // Check if the status is set back to draft, assuming the original status was Ready or Draft
            Assert.Equal(EventStatuses.Draft, eventVEA.Status.Value);
        }

        [Fact]
        public void MakeEventPrivate_WhenEventIsActive_ReturnsFailure()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();
            eventVEA.Activate(); // Assuming this successfully activates the event

            // Act
            var result = eventVEA.MakePrivate();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(result.Errors, e => e.Message.Contains("Cannot make a cancelled or active event private."));
        }
    }
}
