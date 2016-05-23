
namespace YM.Purchasing.Requisitions
{
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
