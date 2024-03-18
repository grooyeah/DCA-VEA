using DCA_VEA.Core.Domain.Common.Values;
using DCA_VEA.Core.Domain.Contracts;
using DCA_VEA.Core.Domain.Entities.Invitation;
using DCA_VEA.Core.Tools.OperationResult;

namespace DCA_VEA.Core.Domain.Services
{
    public class CreatorExtendsInvitationForGuest
        (IGuestRepository guestRepository,
        IEventRepository eventRepository,
        IInvitationRepository invitationRepository)
    {
        // TODO: Add better validation and more logic
        public Result<bool> Handle(InvitationId invitationId, TimeInterval timeInterval)
        {
            var invitation = invitationRepository.Find(invitationId).Payload;

            invitation.UpdateInvitationExpiry(timeInterval.Start, timeInterval.End);
            
            return new Result<bool>(true);
        }
    }
}
