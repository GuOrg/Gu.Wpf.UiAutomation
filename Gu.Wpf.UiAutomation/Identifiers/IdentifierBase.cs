namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    /// Base class for wrappers around various identifiers
    /// </summary>
    public abstract class IdentifierBase : IEquatable<IdentifierBase>
    {
        /// <summary>
        /// Dictionary which holds all known events
        /// </summary>
        private static readonly ConcurrentDictionary<int, EventId> EventCache = new ConcurrentDictionary<int, EventId>();

        /// <summary>
        /// Dictionary which holds all known patterns
        /// </summary>
        private static readonly ConcurrentDictionary<int, PatternId> PatternCache = new ConcurrentDictionary<int, PatternId>();

        /// <summary>
        /// Dictionary which holds all known text attributes
        /// </summary>
        private static readonly ConcurrentDictionary<int, TextAttributeId> TextAttributeCache = new ConcurrentDictionary<int, TextAttributeId>();

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierBase"/> class.
        /// </summary>
        protected IdentifierBase(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// The native id of the identifier
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// A readable name for the identifier
        /// </summary>
        public string Name { get; }

        /// <inheritdoc/>
        public bool Equals(IdentifierBase other)
        {
            return other != null && this.Id.Equals(other.Id);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as IdentifierBase);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Id;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{this.Name} [#{this.Id}]";
        }

        protected static EventId RegisterEvent(int id, string name)
        {
            return EventCache.GetOrAdd(id, x => new EventId(id, name));
        }

        protected static PatternId RegisterPattern(int id, string name, PropertyId availabilityProperty)
        {
            return PatternCache.GetOrAdd(id, x => new PatternId(x, name, availabilityProperty));
        }

        protected static TextAttributeId RegisterTextAttribute(int id, string name)
        {
            return TextAttributeCache.GetOrAdd(id, x => new TextAttributeId(x, name));
        }

        protected static EventId FindEvent(int id)
        {
            if (EventCache.TryGetValue(id, out var eventId))
            {
                return eventId;
            }

            return new EventId(id, $"Event#{id}");
        }

        protected static PatternId FindPattern(int id)
        {
            if (PatternCache.TryGetValue(id, out var patternId))
            {
                return patternId;
            }

            return new PatternId(id, $"Pattern#{id}", null);
        }

        protected static TextAttributeId FindTextAttribute(int id)
        {
            if (TextAttributeCache.TryGetValue(id, out var textAttributeId))
            {
                return textAttributeId;
            }

            return new TextAttributeId(id, $"TextAttribute#{id}");
        }
    }
}
