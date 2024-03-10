using DCA_VEA.Core.Domain.Aggregates.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests.UseCases.Event.UpdateEventDescription
{
    public class UpdateEventDescriptionAggregateTest
    {
        [Fact]
        public void UpdateDescription_WithValidDescription_ShouldUpdateDescription()
        {
            // Arrange
            var validDescription = "This is a valid event description.";
            var eventVEA = EventVEA.CreateEmpty();

            // Act
            eventVEA.UpdateDescription(validDescription);

            // Assert
            Assert.Equal(validDescription, eventVEA.Description.Value);
        }
    }
}
