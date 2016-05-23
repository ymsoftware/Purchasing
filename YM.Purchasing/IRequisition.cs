
namespace YM.Purchasing
{
    public interface IRequisition : IActionable<IRequisition>
    {
        string Id { get; }
        string DepartmentId { get; }
        int Year { get; }
        int Sequence { get; }
        RequisitionStatus Status { get; }
        string Title { get; }

        ExecutionResult Draft();
        ExecutionResult Create();        
    }

    public enum RequisitionStatus
    {
        None,
        Draft,
        Created,
        ApprovedByDepartment,
        ApprovedByFinance,
        Approved,
        RejectedByDepartment,
        RejectedByFinance,
        Rejected
    }
}
