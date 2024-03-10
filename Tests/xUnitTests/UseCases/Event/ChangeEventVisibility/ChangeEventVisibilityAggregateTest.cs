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
            eventVEA.MakePublic(); 

            // Assert
            Assert.Equal(EventTypes.Public, eventVEA.EventType.Value);
            Assert.Equal(initialStatus.Value, eventVEA.Status.Value);
        }

        [Fact]
        public void MakeEventPublic_WhenEventIsCancelled_ThrowsException()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();

            eventVEA.Status.SetValue(EventStatuses.Cancelled);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => eventVEA.MakePublic());

            Assert.Equal("A cancelled event cannot be modified.", exception.Message);
        }

        [Fact]
        public void MakeEventPrivate_MakesPrivate()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();

            // Act
            eventVEA.MakePrivate(); // Assuming this method exists

            // Assert
            Assert.True(eventVEA.EventType.Value == EventTypes.Private);
        }

        [Fact]
        public void MakeEventPrivate_WhenEventCancelled_ThrowsException()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();
            eventVEA.EventType.SetValue(EventTypes.Public);
            eventVEA.Status.SetValue(EventStatuses.Cancelled);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => eventVEA.MakePrivate());
            Assert.Equal("A cancelled event cannot be modified.", exception.Message);
        }
    }
}
