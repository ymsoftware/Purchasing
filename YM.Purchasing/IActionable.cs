
namespace YM.Purchasing
{
    public interface IActionable<T>
    {
        IEntityAction<T>[] AuthorizedActions(string userId);
    }
}
