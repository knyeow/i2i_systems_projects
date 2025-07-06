using Akka.Actor;
using Akka.DependencyInjection;
using AkkaActorSystemWebApi.Actors;
using AkkaActorSystemWebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AkkaActorSystemWebApi;
using AkkaActorSystemWebApi.Messages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ActorStatusStore>();
builder.Services.AddTransient<SupervisorActor>();
builder.Services.AddTransient<LoggerActor>();
builder.Services.AddTransient<WorkerActor>();
builder.Services.AddTransient<MonitorActor>();



builder.Services.AddSingleton<ActorSystem>(provider =>
{
    var bootstrap = BootstrapSetup.Create();
    var diSetup = DependencyResolverSetup.Create(provider);
    var actorSystemSetup = bootstrap.And(diSetup);
    var system = ActorSystem.Create("MySystem", actorSystemSetup);

    var resolver = DependencyResolver.For(system);
    var monitor = system.ActorOf(resolver.Props<MonitorActor>(), "monitor");
    var worker = system.ActorOf(resolver.Props<WorkerActor>(), "worker");
    var manager = system.ActorOf(resolver.Props<ManagerActor>(worker), "manager");
    //var supervisorProps = resolver.Props<SupervisorActor>();
    var supervisor = system.ActorOf(resolver.Props<SupervisorActor>(), "supervisor");

    ActorReferences.Monitor = monitor;
    ActorReferences.Worker = worker;
    ActorReferences.Manager = manager;

    manager.Tell(new TaskMessage("HELLO WORLD"));

    ActorReferences.Supervisor = supervisor;
    return system;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
var app = builder.Build();
app.UseCors();

var system = app.Services.GetRequiredService<ActorSystem>();


app.MapControllers();

// app.Lifetime.ApplicationStopping.Register(() =>
// {
//     var system = app.Services.GetRequiredService<ActorSystem>();
//     system.Terminate().Wait();
// });

app.Run();