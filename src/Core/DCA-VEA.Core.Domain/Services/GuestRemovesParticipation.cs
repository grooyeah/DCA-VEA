using DCA_VEA.Core.Domain.Contracts;
using DCA_VEA.Core.Domain.Entities.Invitation;
using DCA_VEA.Core.Tools.OperationResult;

namespace DCA_VEA.Core.Domain.Services
{
    public class GuestRemovesParticipation
        (IGuestRepository guestRepository,
         IEventRepository eventRepository,
         IInvitationRepository invitationRepository)
    {
        // TODO: Add better validation and more logic
        public Result<bool> Handle(InvitationId invitationId)
        {
            var invitation = invitationRepository.Find(invitationId).Payload;
            var guest = guestRepository.Find(invitation.GuestId).Payload;
            var eventVea = eventRepository.Find(invitation.EventId).Payload;

            eventVea.RemoveGuest(guest.Id);

            return new Result<bool>(true);
        }
    }
}
