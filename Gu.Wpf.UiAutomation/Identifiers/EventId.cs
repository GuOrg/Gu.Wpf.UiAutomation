namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// A wrapper around the event ids
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{this.Name} [#{this.Id}]")]
    public sealed class EventId
    {
        /// <summary>
        /// Fixed EventId which is used for patterns that are not supported by the framework.
        /// </summary>
        public static readonly EventId NotSupportedByFramework = new EventId(-1, "Not supported");

        private static readonly ConcurrentDictionary<int, EventId> Cache = new ConcurrentDictionary<int, EventId>();

        private EventId(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; }

        public string Name { get; }

        public static EventId GetOrCreate(int id, string name)
        {
            return Cache.GetOrAdd(id, x => new EventId(id, name));
        }

        public static bool TryGet(int id, out EventId result)
        {
            return Cache.TryGetValue(id, out result);
        }

        public static EventId Find(int id)
        {
            if (Cache.TryGetValue(id, out var result))
            {
                return result;
            }

            throw new KeyNotFoundException($"Did not find an event with id: {id}");
        }
    }
}
