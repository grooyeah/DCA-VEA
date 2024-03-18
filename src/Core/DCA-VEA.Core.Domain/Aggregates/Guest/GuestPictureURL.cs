using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;
using System.Text.RegularExpressions;

namespace DCA_VEA.Core.Domain.Aggregates.Guest
{
    public class GuestPictureURL : ValueObject
    {
        public string? Value { get; }

        public GuestPictureURL(string? value)
        {
            Value = value;
        }

        public static Result<GuestPictureURL> Create(string? value)
        {
            return Validate(new GuestPictureURL(value));
        }

        private static Result<GuestPictureURL> Validate(GuestPictureURL guestPictureURL)
        {
            if (string.IsNullOrEmpty(guestPictureURL.Value) || !ValidateURL(guestPictureURL.Value))
            {
                return Result<GuestPictureURL>.Failure(new Error(ErrorCodes.SpecificError, "Cannot add picture with invalid URL."));
            }

            return new Result<GuestPictureURL>(guestPictureURL);
        }

        private static bool ValidateURL(string url)
        {
            string pattern = @"^(https?:\/\/)?([\w\d-]+\.)+[\w\d-]+(\/[\w\d- ./?%&=]*)?$";
            return Regex.IsMatch(url, pattern);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
