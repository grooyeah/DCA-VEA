using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Tools.OperationResult;

namespace DCA_VEA.Core.Domain.Entities.Invitation
{
    public class InvitationId : ValueObject
    {
        public Guid Value { get; }

        public InvitationId(Guid value)
        {
            Value = value;
        }

        public static Result<InvitationId> Create(Guid value)
        {
            return Validate(new InvitationId(value));
        }

        private static Result<InvitationId> Validate(InvitationId invitationId)
        {
            if (invitationId.Value.CompareTo(Guid.Empty) == 0)
            {
                return Result<InvitationId>.Failure(new Error(ErrorCodes.SpecificError, $"Cannot assign empty ID.{invitationId.Value}"));
            }

            return new Result<InvitationId>(invitationId);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
