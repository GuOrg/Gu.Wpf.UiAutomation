namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// A wrapper around text attribute ids
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{this.Name} [#{this.Id}]")]
    public sealed class TextAttributeId
    {
        /// <summary>
        /// Fixed TextAttributeId which is used for patterns that are not supported by the framework.
        /// </summary>
        public static readonly TextAttributeId NotSupportedByFramework = new TextAttributeId(-1, "Not supported", null);

        private static readonly ConcurrentDictionary<int, TextAttributeId> Cache = new ConcurrentDictionary<int, TextAttributeId>();

        private readonly Func<AutomationBase, object, object> converter;

        private TextAttributeId(int id, string name, Func<AutomationBase, object, object> converter)
        {
            this.converter = converter;
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; }

        public string Name { get; }

        public static TextAttributeId GetOrCreate(int id, string name)
        {
            return Cache.GetOrAdd(id, x => new TextAttributeId(id, name, null));
        }

        public static TextAttributeId GetOrCreate(int id, string name, Func<object, object> converter)
        {
            return Cache.GetOrAdd(id, x => new TextAttributeId(id, name, (a, o) => converter(o)));
        }

        public static TextAttributeId GetOrCreate(int id, string name, Func<AutomationBase, object, object> converter)
        {
            return Cache.GetOrAdd(id, x => new TextAttributeId(id, name, converter));
        }

        public static bool TryGet(int id, out TextAttributeId result)
        {
            return Cache.TryGetValue(id, out result);
        }

        public static TextAttributeId Find(int id)
        {
            if (Cache.TryGetValue(id, out var result))
            {
                return result;
            }

            throw new KeyNotFoundException($"Did not find a property with id: {id}");
        }

        public T Convert<T>(AutomationBase automation, object value)
        {
            if (this.converter == null)
            {
                return (T)value;
            }

            return (T)this.converter(automation, value);
        }
    }
}
