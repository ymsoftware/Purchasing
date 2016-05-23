
namespace YM.Purchasing
{
    public interface IRepository<T>
    {
        ExecutionResult Save(T entity);
        T Get(string id);
        ExecutionResult Remove(string id);
    }
}
