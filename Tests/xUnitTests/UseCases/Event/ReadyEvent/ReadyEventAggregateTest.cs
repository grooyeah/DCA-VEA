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
        public void ActivateEvent_MakesActive()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();

            // Act
            eventVEA.Ready();

            // Assert
            Assert.Equal(EventStatuses.Ready, eventVEA.Status.Value);
        }
    }
}
