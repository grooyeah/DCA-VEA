using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;

namespace DCA_VEA.Core.Domain.Aggregates.Event
{
    public class EventVisibility : ValueObject
    {
        public string Value { get; private set; }

        public EventVisibility(string value)
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

            if (!value.Contains("Hidden") || !value.Contains("Public"))
            {
                throw new Exception("No matching visibility type entered.");
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
