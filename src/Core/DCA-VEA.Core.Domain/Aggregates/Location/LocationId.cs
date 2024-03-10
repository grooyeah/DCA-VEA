using DCA_VEA.Core.Domain.Common.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCA_VEA.Core.Domain.Aggregates.Location
{
    public class LocationId : ValueObject
    {
        public Guid Value { get; private set; }

        public LocationId(Guid value) 
        {
            Value = value;
            Validate(value);
        }

        private void Validate(Guid value)
        {
            if (value.CompareTo(Guid.Empty) == 0)
            {
                throw new Exception("Cannot assign empty ID.");
            }
        }

        public void SetValue(Guid value)
        {
            Validate(value);    
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
