using Akka.Actor;
using AkkaActorSystemWebApi.Messages;
using AkkaActorSystemWebApi.Services;

namespace AkkaActorSystemWebApi.Actors
{
    public class LoggerActor : ReceiveActor
    {
        private readonly ActorStatusStore _store;
        public LoggerActor(ActorStatusStore store)
        {
            _store = store;
            _store.UpdateStatus("logger", true);

            Receive<LogMessage>(msg =>
            {
                Console.WriteLine("[Logger] " + msg.Message);
                _store.AddHistory("logger", msg.Message);

            });

            Receive<string>(msg =>
            {
                 Console.WriteLine("[Logger] " + msg);
                _store.AddHistory("logger", msg);
            });

            Receive<ForceCrash>(_ => throw new Exception("LoggerActor crashed"));
        }

        protected override void PostStop()
        {
            _store.UpdateStatus("logger", false);
        }
    }
}