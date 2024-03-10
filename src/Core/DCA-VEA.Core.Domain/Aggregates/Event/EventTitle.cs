using DCA_VEA.Core.Domain.Common.Bases;

namespace DCA_VEA.Core.Domain.Aggregates.Event
{
    public class EventTitle : ValueObject
    {
        public string Value { get; private set; }

        public EventTitle(string value)
        {
            Value = value;
            Validate(Value);
        }

        private void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Title cannot be null or empty.");
            }

            if (value.Length < 3 || value.Length > 75)
            {
                throw new Exception("Title must be between 3 and 75 characters long.");
            }
        }

        public void SetValue(string value)
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
