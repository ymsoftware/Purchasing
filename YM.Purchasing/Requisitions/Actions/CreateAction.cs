
namespace YM.Purchasing.Requisitions.Actions
{
    public class CreateAction : EntityAction<Requisition>
    {
        public override string Name
        {
            get { return Constants.CREATE; }
        }

        public override AuthorizationResponse IsAuthorized(Requisition entity, string userId)
        {
            if (entity.Status == RequisitionStatus.None || entity.Status == RequisitionStatus.Draft)
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

            entity.SetStatus(RequisitionStatus.Created);

            return ExecutionResult.Success();
        }
    }
}
