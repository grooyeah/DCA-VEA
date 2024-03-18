using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;


namespace DCA_VEA.Core.Domain.Aggregates.Event
{
    public class EventId : ValueObject
    {
        public Guid Value { get; }

        public EventId(Guid value)
        {
            Value = value;
        }

        public static Result<EventId> Create(Guid value)
        {
            return Validate(new EventId(value));
        }

        private static Result<EventId> Validate(EventId eventId)
        {
            if (eventId.Value.CompareTo(Guid.Empty) == 0)
            {
                return Result<EventId>.Failure(new Error(ErrorCodes.SpecificError, $"Cannot assign empty ID.{eventId.Value}"));
            }

            return new Result<EventId>(eventId);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
