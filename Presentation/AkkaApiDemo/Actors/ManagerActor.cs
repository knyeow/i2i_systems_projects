using Akka.Actor;
using AkkaActorSystemWebApi.Messages;
using AkkaActorSystemWebApi.Services;

namespace AkkaActorSystemWebApi.Actors
{
    public class ManagerActor : ReceiveActor
    {
        private readonly IActorRef _worker;
        private readonly ActorStatusStore _store;

        public ManagerActor(IActorRef worker, ActorStatusStore store)
        {
            _worker = worker;
            _store = store;
            _store.UpdateStatus("manager", true);

            Receive<TaskMessage>(msg =>
            {
                _store.AddHistory("manager", $"Received: {msg.Payload}");
                _worker.Tell(msg);
            });


            Receive<ProcessTask>(msg =>
            {
                Console.WriteLine("[Manager] Received task: " + msg.TaskName);
                _worker.Tell(msg);
            });

            Receive<LogMessage>(msg =>
            {
                Console.WriteLine("[Manager] Logging via Supervisor: " + msg.Message);
                ActorReferences.Supervisor.Tell(msg);
            });
        }

        protected override void PostStop()
        {
            _store.UpdateStatus("manager", false);
        }
    }
}