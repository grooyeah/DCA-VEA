using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;

namespace DCA_VEA.Core.Domain.Aggregates.Event
{
    public class EventTitle : ValueObject
    {
        public string? Value { get; }

        public EventTitle(string value)
        {
            Value = value;
        }

        public static Result<EventTitle> Create(string? title)
        {
            return Validate(new EventTitle(title));
        }

        private static Result<EventTitle> Validate(EventTitle eventTitle)
        {
            if (eventTitle.Value.Length < 3 || eventTitle.Value.Length > 75 || string.IsNullOrWhiteSpace(eventTitle.Value))
            {
              return Result<EventTitle>.Failure(new Error(ErrorCodes.SpecificError, "Title must be between 3 and 75 characters long."));
            }

            return Result<EventTitle>.Success(eventTitle);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
