using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCA_VEA.Core.Domain.Common.Bases
{
    public abstract class AggregateRoot<TId> : Entity<TId>
    {
        protected AggregateRoot(TId id) : base(id) { }
        protected AggregateRoot() { } // For serialization, EFC
    }
}
