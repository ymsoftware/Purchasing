
namespace YM.Purchasing
{
    public interface IEntityAction<T>
    {
        string Name { get; }
        AuthorizationResponse IsAuthorized(T entity, string userId);
        ExecutionResult Execute(T entity, string userId);
    }
}
