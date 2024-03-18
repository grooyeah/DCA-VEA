using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;

namespace DCA_VEA.Core.Domain.Aggregates.Location
{
    public class LocationId : ValueObject
    {
        public Guid Value { get; private set; }

        public LocationId(Guid value) 
        {
            Value = value;
        }

        public static Result<LocationId> Create(Guid value)
        {
            return Validate(new LocationId(value));
        }

        private static Result<LocationId> Validate(LocationId locationId)
        {
            if (locationId.Value.CompareTo(Guid.Empty) == 0)
            {
                return Result<LocationId>.Failure(new Error(ErrorCodes.SpecificError, $"Cannot assign empty ID.{locationId.Value}"));
            }

            return new Result<LocationId>(locationId);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
