
namespace DCA_VEA.Core.Domain.Common.Bases
{
    public abstract class Entity<TId> where TId : notnull //Strongly typed id
    {
        public TId Id { get; }

        protected Entity(TId id) {
            Id = id;
        }
        protected Entity() { } // For serialization, EFC
    }
}
