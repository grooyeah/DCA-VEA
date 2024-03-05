using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
