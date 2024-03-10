using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;


namespace DCA_VEA.Core.Domain.Aggregates.Event
{
    public class EventDescription : ValueObject
    {
        public string Value { get; private set; }

        public EventDescription(string value)
        {
            Value = value;
            Validate(Value);
        }

        private void Validate(string value)
        {
            if(value.Length <= 0 && value.Length > 250)
            {
                throw new Exception("Description cannot be longer than 250 characters.");
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
