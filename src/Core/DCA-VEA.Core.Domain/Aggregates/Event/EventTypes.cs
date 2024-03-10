
using DCA_VEA.Core.Domain.Common.Bases;

namespace DCA_VEA.Core.Domain.Aggregates.Event
{
    public enum EventTypes
    {
        Public,
        Private
    }

    public class EventType : ValueObject
    {
        public EventTypes Value { get; private set; }

        public EventType(EventTypes value) 
        { 
            Value = value;
        }

        public void SetValue(EventTypes value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
