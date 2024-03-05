using DCA_VEA.Core.Domain.Common.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCA_VEA.Core.Domain.Aggregates.Events
{
    internal class EventVEA : Entity<EventVEAId>
    {
        public string Name { get; set; }
        public EventVEA(EventVEAId id, string name) : base(id)
        {
            Name = name;
        }

    }
}
