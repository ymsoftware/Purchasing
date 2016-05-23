
namespace YM.Purchasing.Requisitions.Actions
{
    public class DraftAction : EntityAction<Requisition>
    {
        public override string Name
        {
            get { return Constants.DRAFT; }
        }

        public override AuthorizationResponse IsAuthorized(Requisition entity, string userId)
        {
            if (entity.Status == RequisitionStatus.None)
            {
                //TODO: get user dept, role, etc
                return AuthorizationResponse.Authorized();
            }

            return AuthorizationResponse.NotAuthorized("Invalid status");
        }

        public override ExecutionResult Execute(Requisition entity, string userId)
        {
            var test = IsAuthorized(entity, userId);
            if (!test.IsAuthorized)
            {
                return ExecutionResult.Failure(test.Text);
            }

            entity.SetStatus(RequisitionStatus.Draft);

            return ExecutionResult.Success();
        }
    }
}
