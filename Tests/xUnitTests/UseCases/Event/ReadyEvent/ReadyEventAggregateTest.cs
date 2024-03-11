using DCA_VEA.Core.Domain.Aggregates.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests.UseCases.Event.ReadyEvent
{
    public class ReadyEventAggregateTest
    {
        [Fact]
        public void ReadyEvent_MakesReady()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();
            eventVEA.UpdateTitle("Test title");
            eventVEA.Activate();

            // Act
            var result = eventVEA.Ready();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(EventStatuses.Ready, eventVEA.Status.Value);
        }
    }
}
