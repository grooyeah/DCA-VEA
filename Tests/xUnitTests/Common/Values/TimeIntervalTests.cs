using DCA_VEA.Core.Domain.Common.Values;

namespace xUnitTests.Common.Values
{
    public class TimeIntervalTests
    {
        [Fact]
        public void Create_ShouldReturnSuccessResult_WhenEndIsAfterStart()
        {
            // Arrange
            var start = DateTime.Now;
            var end = start.AddHours(1);

            // Act
            var result = TimeInterval.Create(start, end);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(start, result.Payload.Start);
            Assert.Equal(end, result.Payload.End);
            Assert.Equal(end - start, result.Payload.Duration);
        }

        [Fact]
        public void Create_ShouldReturnFailureResult_WhenEndIsBeforeStart()
        {
            // Arrange
            var start = DateTime.Now;
            var end = start.AddHours(-1); // End time before start time

            // Act
            var result = TimeInterval.Create(start, end);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Single(result.Errors);
            Assert.Equal(1, result.Errors[0].Code);
            Assert.Equal("End date must be greater than or equal to start date", result.Errors[0].Message);
        }

        [Fact]
        public void ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            var start = new DateTime(2020, 1, 1, 12, 0, 0);
            var end = new DateTime(2020, 1, 1, 13, 0, 0);
            var timeInterval = TimeInterval.Create(start, end).Payload;

            // Act
            var resultString = timeInterval.ToString();

            // Assert
            var expectedString = $"Start: {start}, End: {end}";
            Assert.Equal(expectedString, resultString);
        }

        [Fact]
        public void Duration_ShouldReturnDifferenceBetweenStartAndEnd()
        {
            // Arrange
            var start = DateTime.Now;
            var end = start.AddHours(2);
            var timeInterval = TimeInterval.Create(start, end).Payload;

            // Act
            var duration = timeInterval.Duration;

            // Assert
            Assert.Equal(2, duration.TotalHours);
        }
    }
}
