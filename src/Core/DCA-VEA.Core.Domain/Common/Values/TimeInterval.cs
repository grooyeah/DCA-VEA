using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;

namespace DCA_VEA.Core.Domain.Common.Values
{
    public class TimeInterval : ValueObject
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        private TimeInterval(DateTime start, DateTime end)
        {
            this.Start = start;
            this.End = end;
        }

        // Static factory method to create TimeInterval instances
        public static Result<TimeInterval> Create(DateTime start, DateTime end)
        {
            if (end < start)
            {
                return Result<TimeInterval>.Failure(new Error(1, "End date must be greater than or equal to start date")); //Should we use InvalidArgumentException instead?
            }

            return Result<TimeInterval>.Success(new TimeInterval(start, end));
        }

        //Values for checking equality
        protected override IEnumerable<object> GetEqualityComponents() 
        {
            yield return Start;
            yield return End;
        }

        public override string ToString()
        {
            return $"Start: {Start}, End: {End}";
        }

        // Calculating duration
        public TimeSpan Duration => End - Start;
    }
}
