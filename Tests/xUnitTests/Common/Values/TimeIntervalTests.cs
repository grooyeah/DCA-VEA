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
            var date = eventVEA.Date;
            var newStart = date.Start;
            var newEnd = date.End;
            newStart.AddHours(1);
            newEnd.AddHours(1);

            // Act
            eventVEA.UpdateTimeInterval(newStart, newEnd);

            // Assert
            Assert.Equal(newStart, eventVEA.Date.Start);
            Assert.Equal(newEnd, eventVEA.Date.End);
        }

        [Fact]
        public void UpdateTimeInterval_InvalidTimes_ThrowsException()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();

            var newStart = DateTime.Now.AddDays(4).AddHours(1); 
            var newEnd = DateTime.Now.AddDays(4).AddHours(2); 

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => eventVEA.UpdateTimeInterval(newStart, newEnd));
            Assert.Contains("failed to update", exception.Message.ToLower()); 
        }
    }
}
