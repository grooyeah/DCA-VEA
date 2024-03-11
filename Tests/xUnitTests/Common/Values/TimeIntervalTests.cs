using DCA_VEA.Core.Domain.Aggregates.Event;
using DCA_VEA.Core.Domain.Aggregates.Events;
using DCA_VEA.Core.Domain.Aggregates.Location;
using DCA_VEA.Core.Domain.Common.Values;

namespace xUnitTests.Common.Values
{
    public class TimeIntervalTests
    {
        [Fact]
        public void UpdateTimeInterval_WithinValidHours_UpdatesSuccessfully()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();
            var newStart = eventVEA.Date.Start.AddHours(1); // Corrected to actually update the times
            var newEnd = eventVEA.Date.End.AddHours(1);

            // Act
            var result = eventVEA.UpdateTimeInterval(newStart, newEnd);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(newStart, eventVEA.Date.Start);
            Assert.Equal(newEnd, eventVEA.Date.End);
        }

        [Fact]
        public void UpdateTimeInterval_InvalidTimes_ReturnsFailure()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();
            var newStart = DateTime.Now.AddDays(-1); // Example of an invalid time (in the past)
            var newEnd = DateTime.Now.AddDays(-1).AddHours(1);

            // Act
            var result = eventVEA.UpdateTimeInterval(newStart, newEnd);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(result.Errors, e => e.Message.Contains("Failed to update event time interval")); // Adjust the message based on actual implementation
        }
    }
}
