using Akka.Actor;

namespace AkkaActorSystemWebApi
{
    public static class ActorReferences
    {
        public static IActorRef Supervisor { get; set; } = ActorRefs.Nobody;
        public static IActorRef Manager { get; set; } = ActorRefs.Nobody;
        public static IActorRef Worker { get; set; } = ActorRefs.Nobody;
        public static IActorRef Monitor { get; set; } = ActorRefs.Nobody;
    }
}