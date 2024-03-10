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
            eventVEA.UpdateMaxGuests(maxGuests);

            // Assert
            Assert.Equal(maxGuests, eventVEA.MaxGuests.Value);
        }

        [Fact]
        public void SetMaximumGuests_InvalidAmount_ThrowsException()
        {
            // Arrange
            var eventVEA = EventVEA.CreateEmpty();
            var invalidMaxGuests = 0;

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => eventVEA.UpdateMaxGuests(invalidMaxGuests));
            Assert.Equal("Cannot set the number of guests lower than 5.", exception.Message);
        }
    }
}
