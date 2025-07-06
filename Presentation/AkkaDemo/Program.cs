using Akka.Actor;
using AkkaActorSystem.Actors;

class Program
{
    static void Main(string[] args)
    {
        using var system = ActorSystem.Create("MyActorSystem");

        var supervisor = system.ActorOf(Props.Create(() => new SupervisorActor()), "supervisor");
        var monitor = system.ActorOf(Props.Create(() => new MonitorActor()), "monitor");
        var manager = system.ActorOf(Props.Create(() => new ManagerActor(supervisor, monitor)), "manager");


        // for (int i = 0; i < 2; i++)
        // {
        //     manager.Tell(new TaskMessage("task1"));
        //     manager.Tell(new TaskMessage("hello world"));
        //     manager.Tell(new TaskMessage("some important data"));
        // }

        manager.Tell(new TaskMessage("LOG THİS!!!!!!!!!!!!!!"));
        manager.Tell(new TaskMessage("LOG THİS22222!!!!!!!!!!!!!!"));


        Console.ReadLine();
    }
}
