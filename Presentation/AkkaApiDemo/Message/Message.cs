namespace AkkaActorSystemWebApi.Messages
{
    public class ForceCrash { }
    public class RestartLogger { }
    public class TaskMessage
    {
        public string Payload { get; }
        public TaskMessage(string payload) => Payload = payload;
    }
    public class TaskResult
    {
        public string Result { get; }
        public TaskResult(string result) => Result = result;
    }

    public sealed class LogMessage
    {
        public string Message { get; }
        public LogMessage(string message) => Message = message;
        public override string ToString() => $"LogMessage: {Message}";
    }

    public sealed class ProcessTask
    {
        public string TaskName { get; }
        public ProcessTask(string taskName) => TaskName = taskName;
        public override string ToString() => $"ProcessTask: {TaskName}";
    }
}