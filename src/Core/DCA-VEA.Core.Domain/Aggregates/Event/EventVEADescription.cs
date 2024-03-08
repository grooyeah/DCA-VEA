using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCA_VEA.Core.Domain.Aggregates.Event
{
    internal class EventVEADescription : ValueObject
    {
        public string Value { get; }

        private EventVEADescription(string value)
        {
            this.Value = value;
        }

        public static Result<EventVEADescription> Create(string value)
        {
            
            return Result<EventVEADescription>.Success(new EventVEADescription(value));
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
