using DCA_VEA.Core.Domain.Common.Bases;

namespace DCA_VEA.Core.Domain.Aggregates.Events
{
    public enum EventStatuses
    {
        Draft,
        Ready,
        Active,
        Cancelled
    }

    public class EventStatus : ValueObject
    {
        public EventStatuses Value { get; private set; }

        public EventStatus(EventStatuses value)
        {
            Value = value;
        }

        public void SetValue(EventStatuses value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
