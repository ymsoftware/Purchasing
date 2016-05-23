
namespace YM.Purchasing.Requisitions.Actions
{
    public class CreateAction : IEntityAction<IRequisition>
    {
        public string Name
        {
            get { return Constants.CREATE; }
        }

        public AuthorizationResponse IsAuthorized(IRequisition entity, string userId)
        {
            if (entity.Status == RequisitionStatus.None || entity.Status == RequisitionStatus.Draft)
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
       
            return entity.Create();
        }
    }
}
