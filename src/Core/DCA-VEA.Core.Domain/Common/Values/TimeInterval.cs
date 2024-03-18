using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;

namespace DCA_VEA.Core.Domain.Common.Values
{
    public class TimeInterval : ValueObject
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public TimeSpan Duration => End - Start;

        public TimeInterval(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public static Result<TimeInterval> Create(DateTime start, DateTime end)
        {
            return Validate(new TimeInterval(start, end));
        }
        private static Result<TimeInterval> Validate(TimeInterval timeInterval)
        {
            if (timeInterval.End <= timeInterval.Start)
            {
                return Result<TimeInterval>.Failure(new Error(ErrorCodes.SpecificError, "End time must be after start time."));
            }

            if (timeInterval.Start.TimeOfDay < new TimeSpan(8, 0, 0) || (timeInterval.End.TimeOfDay > new TimeSpan(1, 0, 0) && timeInterval.Start.Date == timeInterval.End.Date))
            {
                return Result<TimeInterval>.Failure(new Error(ErrorCodes.SpecificError, "Event must start after 08:00 AM and end by 01:00 AM the next day at the latest."));
            }

            if ((timeInterval.End - timeInterval.Start).TotalHours > 10)
            {
                return Result<TimeInterval>.Failure(new Error(ErrorCodes.SpecificError, "Event duration must be 10 hours or less."));
            }

            if (timeInterval.Start < DateTime.Now)
            {
                return Result<TimeInterval>.Failure(new Error(ErrorCodes.SpecificError,"Event start time cannot be in the past."));
            }

            return new Result<TimeInterval>(timeInterval);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }
    }
}
