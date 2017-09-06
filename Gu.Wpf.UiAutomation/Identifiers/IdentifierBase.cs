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

        protected static TextAttributeId RegisterTextAttribute(int id, string name)
        {
            return TextAttributeCache.GetOrAdd(id, x => new TextAttributeId(x, name));
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
