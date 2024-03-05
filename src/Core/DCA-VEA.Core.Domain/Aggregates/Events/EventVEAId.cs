using DCA_VEA.Core.Domain.Common.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCA_VEA.Core.Domain.Aggregates.Events
{
    internal class EventVEAId : ValueObject // Strongly Typed ID Value Object
    {
        public Guid Value { get; private set; }

        public EventVEAId(Guid value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator Guid(EventVEAId userId) => userId.Value;
        public static implicit operator EventVEAId(Guid value) => new EventVEAId(value);
    }
}
