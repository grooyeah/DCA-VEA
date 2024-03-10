using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;

namespace DCA_VEA.Core.Domain.Aggregates.Events
{
    public class EventId : ValueObject 
    {
        public Guid Value { get; }

        public EventId(Guid value)
        {
            Value = value;
            Validate(Value);
        }

        private void Validate(Guid value)
        {
            if(value.CompareTo(Guid.Empty) == 0) 
            {
                throw new Exception($"Cannot assign empty ID.{value}");
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
