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
            eventVEA.Activate(); 

            // Assert
            Assert.Equal(EventStatuses.Active, eventVEA.Status.Value);
        }
    }
}
