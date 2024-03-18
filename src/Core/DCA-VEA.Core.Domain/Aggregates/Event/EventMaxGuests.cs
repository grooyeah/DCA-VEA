using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;

namespace DCA_VEA.Core.Domain.Aggregates.Event
{
    public class EventMaxGuests : ValueObject
    {
        public int Value { get; }

        public EventMaxGuests(int value)
        { 
            Value = value;
        }

        public static Result<EventMaxGuests> Create(int maxGuests)
        {
            return Validate(new EventMaxGuests(maxGuests));
        }

        private static Result<EventMaxGuests> Validate(EventMaxGuests eventMaxGuests)
        {
            if (eventMaxGuests.Value < 5 || eventMaxGuests.Value > 50)
            {
                return Result<EventMaxGuests>.Failure(new Error(ErrorCodes.SpecificError,"Cannot set the number of max guests that is not between 5 and 50."));
            }

            return new Result<EventMaxGuests>(eventMaxGuests);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
