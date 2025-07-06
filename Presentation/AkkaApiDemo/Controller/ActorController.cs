using Akka.Actor;
using AkkaActorSystemWebApi.Messages;
using AkkaActorSystemWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AkkaActorSystemWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorController : ControllerBase
    {
        private static int taskCounter,logCounter = 0;
        private readonly ActorStatusStore _store;

        public ActorController(ActorStatusStore store)
        {
            _store = store;
        }

        [HttpGet("status")]
        public IActionResult Status() => Ok(_store.GetAllStatuses());

        [HttpPost("task")]
        public IActionResult RandomTaskMessage()
        {
            int taskId = taskCounter++;
            TaskMessage taskMessage = new TaskMessage($"Task id: {taskId}");
            ActorReferences.Manager.Tell(taskMessage);
            return Ok();
        }

        [HttpPost("log")]
        public IActionResult RandomLogMessage()
        {
            int logId = logCounter++;
            LogMessage taskMessage = new LogMessage($"Log id: {logId}");
            ActorReferences.Manager.Tell(taskMessage);
            return Ok();
        }

        [HttpPost("crash")]
        public IActionResult CrashLogger()
        {
            ActorReferences.Supervisor.Tell(new ForceCrash());
            return Ok();
        }

        [HttpPost("restart")]
        public IActionResult RestartLogger()
        {
            ActorReferences.Supervisor.Tell(new RestartLogger());
            return Ok();
        }
    }
}
