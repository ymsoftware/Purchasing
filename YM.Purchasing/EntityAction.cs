
namespace YM.Purchasing
{
    public abstract class EntityAction<T>
    {
        public abstract string Name { get; }
        public abstract AuthorizationResponse IsAuthorized(T entity, string userId);
        public abstract ExecutionResult Execute(T entity, string userId);
    }
}
