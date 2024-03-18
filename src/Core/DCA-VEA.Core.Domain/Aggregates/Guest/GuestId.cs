using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;

namespace DCA_VEA.Core.Domain.Aggregates.Guest
{
    public class GuestId : ValueObject
    {
        public Guid Value { get; }

        public GuestId(Guid value)
        {
            Value = value;
        }

        public static Result<GuestId> Create(Guid value)
        {
            return Validate(new GuestId(value));
        }

        private static Result<GuestId> Validate(GuestId eventId)
        {
            if (eventId.Value.CompareTo(Guid.Empty) == 0)
            {
                return Result<GuestId>.Failure(new Error(ErrorCodes.SpecificError, $"Cannot assign empty ID.{eventId.Value}"));
            }

            return new Result<GuestId>(eventId);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
