
namespace DCA_VEA.Core.Domain.Common.Bases
{
    public abstract class ValueObject
    {
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return ((ValueObject)obj).GetEqualityComponents().SequenceEqual(GetEqualityComponents());
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override int GetHashCode()
        {
            unchecked // use unchecked to prevent overflow exceptions
            {
                return GetEqualityComponents()
                    .Where(x => x != null) // filter out null values to avoid NullReferenceException
                    .Select(x => x!.GetHashCode())
                    .Aggregate(17, (x, y) => x * 23 + y); // 17 and 23 are arbitrary prime numbers used for hashing
            }
        }

        // Optional: Equals operator overloads for syntactic sugar
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(ValueObject left, ValueObject right) => !(left == right);
    }
}
