using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;
using System.Text.RegularExpressions;

namespace DCA_VEA.Core.Domain.Aggregates.Guest
{
    public class GuestLastName : ValueObject
    {
        public string? Value { get; }

        public GuestLastName(string value)
        {
            Value = value;
        }

        public static Result<GuestLastName> Create(string? lastName)
        {
            return Validate(new GuestLastName(lastName));
        }

        private static Result<GuestLastName> Validate(GuestLastName guestLastName)
        {
            if (string.IsNullOrWhiteSpace(guestLastName.Value) || !ValidateLastName(guestLastName.Value))
            {
                return Result<GuestLastName>.Failure(new Error(ErrorCodes.SpecificError, "First name must be between 2 and 25 characters."));
            }

            return new Result<GuestLastName>(guestLastName);
        }

        private static bool ValidateLastName(string value)
        {
            return value.Length >= 2 && value.Length <= 25 && Regex.IsMatch(value, @"^[A-Za-z]+$");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
