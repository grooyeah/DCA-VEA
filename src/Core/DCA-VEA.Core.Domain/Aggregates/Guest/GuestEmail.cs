using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;
using System.Text.RegularExpressions;

namespace DCA_VEA.Core.Domain.Aggregates.Guest
{
    public class GuestEmail : ValueObject
    {
        public string? Value { get; }

        public GuestEmail(string value) 
        {
            Value = value;
        } 

        public static Result<GuestEmail> Create(string? email)
        {
            return Validate(new GuestEmail(email));
        }

        private static Result<GuestEmail> Validate(GuestEmail guestEmail)
        {
            if(string.IsNullOrWhiteSpace(guestEmail.Value) || !ValidateEmail(guestEmail.Value)) 
            {
                return Result<GuestEmail>.Failure(new Error(ErrorCodes.SpecificError, "Email must end with @via.dk, be in a standard format, " +
                    "have a 3-6 character local part that's either 3-4 letters or 6 digits."));
            }

            return new Result<GuestEmail>(guestEmail);
        }

        private static bool ValidateEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            bool endsWithViaDk = email.EndsWith("@via.dk");

            string localPart = email.Split('@')[0];

            bool localPartLength = localPart.Length >= 3 && localPart.Length <= 6;

            bool localPartContent = Regex.IsMatch(localPart, @"^([A-Za-z]{3,4}|[0-9]{6})$");

            return Regex.IsMatch(email, emailPattern) && endsWithViaDk && localPartLength && localPartContent;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
