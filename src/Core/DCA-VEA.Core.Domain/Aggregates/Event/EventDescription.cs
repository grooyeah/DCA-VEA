using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;


namespace DCA_VEA.Core.Domain.Aggregates.Event
{
    public class EventDescription : ValueObject
    {
        public string? Value { get;}

        public EventDescription(string value)
        {
            Value = value;
        }

        public static Result<EventDescription> Create(string? description)
        {
            return Validate(new EventDescription(description));
        }

        private static Result<EventDescription> Validate(EventDescription eventDescription)
        {
            if(eventDescription.Value.Length <= 0 || eventDescription.Value.Length > 250 || string.IsNullOrWhiteSpace(eventDescription.Value))
            {
                return Result<EventDescription>.Failure(new Error(ErrorCodes.SpecificError, "Description must be between 3 and 250 characters."));
            }

            return Result<EventDescription>.Success(eventDescription);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
