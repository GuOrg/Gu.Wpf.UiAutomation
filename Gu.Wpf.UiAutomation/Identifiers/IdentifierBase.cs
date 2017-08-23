namespace Gu.Wpf.UiAutomation.Identifiers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Base class for wrappers around various identifiers
    /// </summary>
    public abstract class IdentifierBase : IEquatable<IdentifierBase>
    {
        /// <summary>
        /// Class which capsules all identifiers which can be used for an automation library
        /// </summary>
        private class IdentifiersHolder
        {
            /// <summary>
            /// Dictionary which holds all known properties
            /// </summary>
            public readonly Dictionary<int, PropertyId> PropertyDict = new Dictionary<int, PropertyId>();

            /// <summary>
            /// Dictionary which holds all known events
            /// </summary>
            public readonly Dictionary<int, EventId> EventDict = new Dictionary<int, EventId>();

            /// <summary>
            /// Dictionary which holds all known patterns
            /// </summary>
            public readonly Dictionary<int, PatternId> PatternDict = new Dictionary<int, PatternId>();

            /// <summary>
            /// Dictionary which holds all known text attributes
            /// </summary>
            public readonly Dictionary<int, TextAttributeId> TextAttributeDict = new Dictionary<int, TextAttributeId>();
        }

        private static readonly IdentifiersHolder Identifiers = new IdentifiersHolder();

        /// <summary>
        /// The native id of the identifier
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// A readable name for the identifier
        /// </summary>
        public string Name { get; }

        protected IdentifierBase(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

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

        protected static PropertyId RegisterProperty(int id, string name)
        {
            return Register(id, Identifiers.PropertyDict, () => new PropertyId(id, name));
        }

        protected static EventId RegisterEvent(int id, string name)
        {
            return Register(id, Identifiers.EventDict, () => new EventId(id, name));
        }

        protected static PatternId RegisterPattern(int id, string name, PropertyId availabilityProperty)
        {
            return Register(id, Identifiers.PatternDict, () => new PatternId(id, name, availabilityProperty));
        }

        protected static TextAttributeId RegisterTextAttribute(int id, string name)
        {
            return Register(id, Identifiers.TextAttributeDict, () => new TextAttributeId(id, name));
        }

        protected static PropertyId FindProperty(int id)
        {
            if (Identifiers.PropertyDict.TryGetValue(id, out var propertyId))
            {
                return propertyId;
            }

            return new PropertyId(id, $"Property#{id}");
        }

        protected static EventId FindEvent(int id)
        {
            if (Identifiers.EventDict.TryGetValue(id, out var eventId))
            {
                return eventId;
            }

            return new EventId(id, $"Event#{id}");
        }

        protected static PatternId FindPattern(int id)
        {
            if (Identifiers.PatternDict.TryGetValue(id, out var patternId))
            {
                return patternId;
            }

            return new PatternId(id, $"Pattern#{id}", null);
        }

        protected static TextAttributeId FindTextAttribute(int id)
        {
            if (Identifiers.TextAttributeDict.TryGetValue(id, out var textAttributeId))
            {
                return textAttributeId;
            }

            return new TextAttributeId(id, $"TextAttribute#{id}");
        }

        /// <summary>
        /// Adds the property to the dictionary if it does not exist yet
        /// </summary>
        private static T Register<T>(int commonId, IDictionary<int, T> dict, Func<T> creator)
        {
            if (!dict.TryGetValue(commonId, out var id))
            {
                id = creator();
                dict[commonId] = id;
            }

            return id;
        }
    }
}
