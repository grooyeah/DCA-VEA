using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;
using System.Text.RegularExpressions;

namespace DCA_VEA.Core.Domain.Aggregates.Guest
{
    public class GuestFirstName : ValueObject
    {
        public string? Value { get;}

        public GuestFirstName(string value)
        {
            Value = value;
        }

        private static Result<GuestFirstName> Create(string? firstName)
        {
            return Validate(new GuestFirstName(firstName));
        }

        private static Result<GuestFirstName> Validate(GuestFirstName guestFirstName)
        {
            if (string.IsNullOrWhiteSpace(guestFirstName.Value) || !ValidateFirstName(guestFirstName.Value))
            {
                return Result<GuestFirstName>.Failure(new Error(ErrorCodes.SpecificError, "First name must be between 2 and 25 characters."));
            }

            return new Result<GuestFirstName>(guestFirstName);
        }

        private static bool ValidateFirstName(string value)
        {
            return value.Length >= 2 && value.Length <= 25 && Regex.IsMatch(value, @"^[A-Za-z]+$");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
