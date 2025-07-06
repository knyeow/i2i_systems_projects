using Akka.Actor;
using AkkaActorSystemWebApi.Messages;
using AkkaActorSystemWebApi.Services;

namespace AkkaActorSystemWebApi.Actors
{
    public class WorkerActor : ReceiveActor
    {
        private readonly ActorStatusStore _store;

        public WorkerActor(ActorStatusStore store)
        {
            _store = store;
            _store.UpdateStatus("worker", true);


            Receive<ProcessTask>(msg =>
            {
                Console.WriteLine($"[Worker] Processing task: {msg.TaskName}");
                ActorReferences.Supervisor.Tell(new LogMessage($"Task '{msg.TaskName}' processed by Worker."));
            });

            Receive<TaskMessage>(msg =>
            {
                _store.AddHistory("worker", $"Processing: {msg.Payload}");
                ActorReferences.Monitor.Tell(new TaskResult($"Result: {msg.Payload}"));
            });
        }

        protected override void PostStop()
        {
            _store.UpdateStatus("worker", false);
        }
    }
}