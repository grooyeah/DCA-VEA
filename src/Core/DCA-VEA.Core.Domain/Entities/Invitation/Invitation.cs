using DCA_VEA.Core.Domain.Aggregates.Event;
using DCA_VEA.Core.Domain.Aggregates.Guest;
using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Domain.Common.Values;
using DCA_VEA.Core.Tools.OperationResult;

namespace DCA_VEA.Core.Domain.Entities.Invitation
{
    public class Invitation : Entity<InvitationId>
    {
        internal EventId EventId { get; private set; }
        internal GuestId GuestId { get; private set; }
        internal TimeInterval InvitationExpiry { get; private set; }
        internal InvitationStatus InvitationStatus { get; private set; }

        public Invitation(InvitationId id, EventId eventId, GuestId guestId, 
            TimeInterval invitationExpiry, InvitationStatus invitationStatus)
            : base(id)
        {
            EventId = eventId;
            GuestId = guestId;
            InvitationExpiry = invitationExpiry;
            InvitationStatus = invitationStatus;
        }

        public static Result<Invitation> Create(EventId eventId, GuestId guestId,
                                            TimeInterval invitationExpiry, InvitationStatus invitationStatus)
        {

            var invitaion = new Invitation(InvitationId.Create(Guid.NewGuid()), eventId, guestId, invitationExpiry, invitationStatus);

            return new Result<Invitation>(invitaion);
        }

        // TODO: Add more validation
        public Result<bool> UpdateInvitationExpiry(DateTime start, DateTime end)
        {
            InvitationExpiry = TimeInterval.Create(start, end);
            return new Result<bool>(true);
        }

    }
}
