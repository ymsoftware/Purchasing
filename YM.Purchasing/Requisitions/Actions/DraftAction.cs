
namespace YM.Purchasing.Requisitions.Actions
{
    public class DraftAction : IEntityAction<IRequisition>
    {
        public string Name
        {
            get { return Constants.DRAFT; }
        }

        public AuthorizationResponse IsAuthorized(IRequisition entity, string userId)
        {
            if (entity.Status == RequisitionStatus.None)
            {
                //TODO: get user dept, role, etc
                return AuthorizationResponse.Authorized();
            }

            return AuthorizationResponse.NotAuthorized("Invalid status");
        }

        public ExecutionResult Execute(IRequisition entity, string userId)
        {
            var test = IsAuthorized(entity, userId);
            if (!test.IsAuthorized)
            {
                return ExecutionResult.Failure(test.Text);
            }

            return entity.Draft();
        }
    }
}
