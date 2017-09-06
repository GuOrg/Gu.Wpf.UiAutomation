namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// A wrapper around the pattern ids
    /// </summary>
    public sealed class PatternId
    {
        /// <summary>
        /// Fixed PatternId which is used for patterns that are not supported by the framework.
        /// </summary>
        public static readonly PatternId NotSupportedByFramework = new PatternId(-1, "Not supported", null);

        private static readonly ConcurrentDictionary<int, PatternId> Cache = new ConcurrentDictionary<int, PatternId>();

        private PatternId(int id, string name, PropertyId availabilityProperty)
        {
            this.Id = id;
            this.Name = name;
            this.AvailabilityProperty = availabilityProperty;
        }

        public int Id { get; }

        public string Name { get; }

        /// <summary>
        /// Property which can be used to check for the patterns availability on an element
        /// </summary>
        public PropertyId AvailabilityProperty { get; }

        public static PatternId GetOrCreate(int id, string name, PropertyId availabilityProperty)
        {
            return Cache.GetOrAdd(id, x => new PatternId(x, name, availabilityProperty));
        }

        public static bool TryGet(int id, out PatternId patternId)
        {
            return Cache.TryGetValue(id, out patternId);
        }

        public static PatternId Find(int id)
        {
            if (Cache.TryGetValue(id, out var patternId))
            {
                return patternId;
            }

            throw new KeyNotFoundException($"Did not find a pattern with id: {id}");
        }
    }
}
