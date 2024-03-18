using DCA_VEA.Core.Domain.Aggregates.Guest;
using DCA_VEA.Core.Tools.OperationResult;

namespace DCA_VEA.Core.Domain.Contracts
{
    public interface IGuestRepository
    {
        public Result<GuestVEA> Save(GuestVEA entity);
        public Result<GuestVEA> Find(GuestId id);
        public Result<GuestVEA> Remove(GuestId id);
        public Result<GuestVEA> Update(GuestVEA entity);
    }
}
