using DCA_VEA.Core.Domain.Aggregates.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests.UseCases.Event.ActivateEvent
{
    public class ActivateEventAggregateTest
    {
        [Fact]
        public void ActivateEvent_MakesActive()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();

            // Act
            var result = eventVEA.Activate();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(EventStatuses.Active, eventVEA.Status.Value);
        }
    }
}
