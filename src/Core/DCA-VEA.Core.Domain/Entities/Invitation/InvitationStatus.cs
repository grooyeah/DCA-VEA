using DCA_VEA.Core.Domain.Common.Bases;

namespace DCA_VEA.Core.Domain.Entities.Invitation
{
    public enum InvitationStatuses
    {
        Pending,
        Accepted,
        Declined
    }

    public class InvitationStatus : ValueObject
    {
        public InvitationStatuses Value { get; private set; }

        public InvitationStatus(InvitationStatuses value)
        {
            Value = value;
        }

        public void SetValue(InvitationStatuses value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
