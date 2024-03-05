using DCA_VEA.Core.Domain.Aggregates.Events;
using System.Reflection;

namespace xUnitTests.UseCases
{
    public class CreateEmptyEvent
    {
        [Fact]
        public void CreateEmpty_EventIsCreatedWithDefaultValues()
        {
            // Arrange
            var eventVea = EventVEA.CreateEmpty();

            // Act
            var properties = typeof(EventVEA).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            // Assert
            foreach (var property in properties)
            {
                // Skip properties with backing fields or methods
                if (property.GetGetMethod(true) == null || property.GetSetMethod(true) == null)
                    continue;

                // Get the value of each property using reflection
                var value = property.GetValue(eventVea);

                // Assert default values based on property type
                switch (value)
                {
                    case int intValue:
                        Assert.Equal(0, intValue);
                        break;
                    case bool boolValue:
                        Assert.False(boolValue);
                        break;
                    case string stringValue:
                        Assert.Equal(string.Empty, stringValue);
                        break;
                    case DateTime dateTimeValue:
                        Assert.Equal(default(DateTime), dateTimeValue);
                        break;
                    case EventVEAStatus statusValue:
                        Assert.Equal(EventVEAStatus.Draft, statusValue);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
