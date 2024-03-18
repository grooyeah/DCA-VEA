using DCA_VEA.Core.Domain.Entities.Invitation;
using DCA_VEA.Core.Tools.OperationResult;

namespace DCA_VEA.Core.Domain.Contracts
{
    public interface IInvitationRepository
    {
        public Result<Invitation> Save(Invitation entity);
        public Result<Invitation> Find(InvitationId id);
        public Result<Invitation> Remove(InvitationId id);
        public Result<Invitation> Update(Invitation entity);
    }
}
