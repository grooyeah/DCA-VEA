using DCA_VEA.Core.Domain.Aggregates.Event;
using DCA_VEA.Core.Domain.Aggregates.Events;
using DCA_VEA.Core.Tools.OperationResult;


namespace DCA_VEA.Core.Domain.Contracts
{
    public interface IEventRepository
    {
        public Result<EventVEA> Save(EventVEA entity);
        public Result<EventVEA> Find(EventId id);
        public Result<EventVEA> Remove(EventId id);
        public Result<EventVEA> Update(EventVEA entity);
    }
}
