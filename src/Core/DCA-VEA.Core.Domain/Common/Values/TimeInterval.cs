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
            Validate(start, end);
            Start = start;
            End = end;
        }

        private void Validate(DateTime start, DateTime end)
        {
            if (end <= start)
            {
                throw new InvalidOperationException("End time must be after start time.");
            }

            if (start.TimeOfDay < new TimeSpan(8, 0, 0) || (end.TimeOfDay > new TimeSpan(1, 0, 0) && start.Date == end.Date))
            {
                throw new InvalidOperationException("Event must start after 08:00 AM and end by 01:00 AM the next day at the latest.");
            }

            if ((end - start).TotalHours > 10)
            {
                throw new InvalidOperationException("Event duration must be 10 hours or less.");
            }

            if (start < DateTime.Now)
            {
                throw new InvalidOperationException("Event start time cannot be in the past.");
            }
        }

        public void SetValue(DateTime start, DateTime end)
        {
            Validate(start, end);
            Start = start;
            End = end;
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }
    }
}
