using DCA_VEA.Core.Domain.Common.Bases;

namespace DCA_VEA.Core.Domain.Aggregates.Event
{
    public class EventMaxGuests : ValueObject
    {
        public int Value { get; private set; }

        public EventMaxGuests(int value)
        { 
            Value = value;
            Validate(Value);
        }

        private void Validate(int value)
        {
            if (value < 5 || value > 50)
            {
                throw new Exception("Cannot set the number of max guests that is not between 5 and 50.");
            }
        }

        public void SetValue(int value)
        {
            Validate(value);
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
