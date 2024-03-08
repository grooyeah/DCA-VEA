using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;

namespace DCA_VEA.Core.Domain.Aggregates.Event
{
    internal class EventVEATitle : ValueObject
    {
        public string Value { get; }

        private EventVEATitle(string value)
        {
            this.Value = value;
        }

        public static Result<EventVEATitle> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Result<EventVEATitle>.Failure(new Error(1, "Title cannot be null or empty."));
            }

            if (value.Length < 3 || value.Length > 75)
            {
                return Result<EventVEATitle>.Failure(new Error(2, "Title must be between 3 and 75 characters long."));
            }

            return Result<EventVEATitle>.Success(new EventVEATitle(value));
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
