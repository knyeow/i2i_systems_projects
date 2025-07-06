using Akka.Actor;
using AkkaActorSystemWebApi.Messages;
using AkkaActorSystemWebApi.Services;

namespace AkkaActorSystemWebApi.Actors
{
    public class MonitorActor : ReceiveActor
    {
        private readonly ActorStatusStore _store;
        private readonly System.Timers.Timer _timer;

        public MonitorActor(ActorStatusStore store)
        {
            _store = store;
            _store.UpdateStatus("monitor", true);


            _timer = new System.Timers.Timer(10000);
            _timer.Elapsed += (_, _) => Console.WriteLine("[Monitor] Monitoring heartbeat at " + DateTime.Now);
            _timer.AutoReset = true;
            _timer.Enabled = true;

            Receive<TaskResult>(msg =>
            {
                _store.AddHistory("monitor", msg.Result);
                ActorReferences.Supervisor.Tell(msg.Result);
            });
        }

        protected override void PostStop()
        {
            _store.UpdateStatus("monitor", false);
            _timer?.Stop();
            _timer?.Dispose();
        }
    }
}
