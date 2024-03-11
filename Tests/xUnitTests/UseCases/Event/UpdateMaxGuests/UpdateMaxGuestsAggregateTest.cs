using DCA_VEA.Core.Domain.Aggregates.Events;

namespace xUnitTests.UseCases.Event.UpdateMaxGuests
{
    public class UpdateMaxGuestsAggregateTest
    {

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(25)]
        [InlineData(50)]
        public void SetMaximumGuests_WithinValidRange_UpdatesMaxGuests(int maxGuests)
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty(); // Assuming draft status by default

            // Act
            var result = eventVEA.UpdateMaxGuests(maxGuests);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(maxGuests, eventVEA.MaxGuests.Value);
        }

        [Fact]
        public void SetMaximumGuests_InvalidAmount_ReturnsFailure()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();
            var invalidMaxGuests = 0;

            // Act
            var result = eventVEA.UpdateMaxGuests(invalidMaxGuests);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(result.Errors, e => e.Message == "Cannot set the number of guests lower than 5.");
        }
    }
}
