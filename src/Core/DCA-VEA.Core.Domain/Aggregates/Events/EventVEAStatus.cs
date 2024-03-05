using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCA_VEA.Core.Domain.Aggregates.Events
{
    internal enum EventVEAStatus
    {
        Draft,
        Created,
        Active,
        Cancelled,
        Readied
    }
}
