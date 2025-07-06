using System.Collections.Concurrent;

namespace AkkaActorSystemWebApi.Services
{
    public class ActorStatusStore
    {
        private readonly ConcurrentDictionary<string, (bool alive, Queue<string> history)> _status = new();

        public void UpdateStatus(string actor, bool alive)
        {
            _status.AddOrUpdate(actor,
                _ => (alive, new Queue<string>()),
                (_, tuple) => (alive, tuple.history));
        }

        public void AddHistory(string actor, string message)
        {
            var history = _status.GetOrAdd(actor, _ => (true, new Queue<string>())).history;
            lock (history)
            {
                if (history.Count >= 4)
                    history.Dequeue();
                history.Enqueue(message);
            }
        }

        public object GetAllStatuses() => _status.ToDictionary(
            kvp => kvp.Key,
            kvp => new { alive = kvp.Value.alive, history = kvp.Value.history.ToArray() });
    }
}
