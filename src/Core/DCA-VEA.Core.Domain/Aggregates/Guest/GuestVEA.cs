using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;

namespace DCA_VEA.Core.Domain.Aggregates.Guest
{
    public class GuestVEA : AggregateRoot<GuestId>
    {
        internal GuestFirstName FirstName { get; private set; }
        internal GuestLastName LastName { get;private set; }
        internal GuestEmail Email { get; private set; }
        internal GuestPictureURL PictureURL { get; private set; }

        public GuestVEA(GuestId guestId, GuestFirstName guestFirstName, GuestLastName guestLastName,
                        GuestEmail guestEmail, GuestPictureURL guestPictureURL) : base(guestId)
        {
            FirstName = guestFirstName;
            LastName = guestLastName;
            Email = guestEmail;
            PictureURL = guestPictureURL;
        }

        public static Result<GuestVEA> Create(GuestFirstName guestFirstName, GuestLastName guestLastName,
                                              GuestEmail guestEmail, GuestPictureURL pictureURL)
        {
            if(string.IsNullOrWhiteSpace(guestFirstName.Value))
            {
                return Result<GuestVEA>.Failure(new Error(ErrorCodes.SpecificError, "First name cannot be null or empty."));
            }

            if(string.IsNullOrWhiteSpace(guestLastName.Value))
            {
                return Result<GuestVEA>.Failure(new Error(ErrorCodes.SpecificError, "Last name cannot be null or empty."));

            }

            if (string.IsNullOrWhiteSpace(guestEmail.Value))
            {
                return Result<GuestVEA>.Failure(new Error(ErrorCodes.SpecificError, "Email cannot be null or empty."));
            }

            var guest = new GuestVEA(GuestId.Create(Guid.NewGuid()), guestFirstName, guestLastName, guestEmail, pictureURL);
            
            return new Result<GuestVEA>(guest);
        }
    }
}
