using Akka.Actor;
using Akka.DependencyInjection;
using AkkaActorSystemWebApi.Messages;
using AkkaActorSystemWebApi.Services;

namespace AkkaActorSystemWebApi.Actors
{
    public class SupervisorActor : ReceiveActor
    {
        private IActorRef _logger;
        private readonly ActorStatusStore _store;

        public SupervisorActor(ActorStatusStore store)
        {
            Console.WriteLine("[SupervisorActor] Constructor invoked");

            _store = store;
            _store.UpdateStatus("supervisor", true);

            Become(Ready);
            StartLogger();
        }

        private void Ready()
        {
            Receive<RestartLogger>(_ =>
            {
                Console.WriteLine("[SupervisorActor] RestartLogger received");
                _store.AddHistory("supervisor", "Logger Restarted");
                StartLogger();
            });

            Receive<Terminated>(_ =>
            {
                Console.WriteLine("[SupervisorActor] Logger terminated");
                 _store.AddHistory("supervisor", "Logger terminated");

                _store.UpdateStatus("logger", false);
            });

            ReceiveAny(message =>
            {
                Console.WriteLine("[SupervisorActor] Received message: " + message);
                _logger?.Forward(message);
            });
        }
        private void StartLogger()
        {
            if (_logger != null)
            {
                Console.WriteLine("[SupervisorActor] Stopping existing logger");
                _store.AddHistory("supervisor", "Stopping Logger");

                Context.Unwatch(_logger);
                Context.Stop(_logger);
            }

            Console.WriteLine("[SupervisorActor] Starting new logger");
            var resolver = DependencyResolver.For(Context.System);
            _logger = Context.ActorOf(resolver.Props<LoggerActor>(), "logger");
             _store.AddHistory("supervisor", "New  logger created");
            _logger.Tell(new LogMessage("logla"));
            Context.Watch(_logger);
            _store.UpdateStatus("logger", true);
        }

        protected override SupervisorStrategy SupervisorStrategy() => new OneForOneStrategy(
            maxNrOfRetries: 0,
            withinTimeRange: TimeSpan.FromSeconds(1),
            decider: Decider.From(ex =>
            {
                Console.WriteLine("[SupervisorActor] Exception caught: " + ex.Message);
                _store.UpdateStatus("logger", false);
                return Directive.Stop;
            }));
    }
}