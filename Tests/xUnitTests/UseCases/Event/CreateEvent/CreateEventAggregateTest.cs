using DCA_VEA.Core.Domain.Aggregates.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests.UseCases.Event.CreateEvent
{
    public class CreateEventAggregateTest
    {
        [Fact]
        public void CreateEmptyEvent_ShouldSetDefaults()
        {
            // Arrange & Act
            var eventVEA = EventVEA.CreateEmpty();

            // Assert
            Assert.NotNull(eventVEA.Id);
            Assert.Equal("Working Title", eventVEA.Title.Value);
            Assert.Equal("", eventVEA.Description.Value);
            Assert.Equal(5, eventVEA.MaxGuests.Value);
            Assert.Equal(EventStatuses.Draft, eventVEA.Status.Value);
        }
    }
}
