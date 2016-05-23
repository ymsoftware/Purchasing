
namespace YM.Purchasing
{
    public class ExecutionResult
    {
        public ExecutionStatus Status { get; private set; }
        public string Text { get; private set; }

        public ExecutionResult(ExecutionStatus status, string text = null)
        {
            Status = status;
            Text = text;
        }

        public static ExecutionResult Success()
        {
            return new ExecutionResult(ExecutionStatus.Success);
        }

        public static ExecutionResult Failure(string text)
        {
            return new ExecutionResult(ExecutionStatus.Failure, text);
        }
    }
}
