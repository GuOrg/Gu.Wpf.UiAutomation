namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// A wrapper around the property ids
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{this.Name} [#{this.Id}]")]
    public sealed class PropertyId
    {
        /// <summary>
        /// Fixed PropertyId which is used for patterns that are not supported by the framework.
        /// </summary>
        public static readonly PropertyId NotSupportedByFramework = new PropertyId(-1, "Not supported", null);

        private static readonly ConcurrentDictionary<int, PropertyId> Cache = new ConcurrentDictionary<int, PropertyId>();

        private readonly Func<AutomationBase, object, object> converter;

        private PropertyId(int id, string name, Func<AutomationBase, object, object> converter)
        {
            this.converter = converter;
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; }

        public string Name { get; }

        public static PropertyId GetOrCreate(int id, string name)
        {
            return Cache.GetOrAdd(id, x => new PropertyId(id, name, null));
        }

        public static PropertyId GetOrCreate(int id, string name, Func<object, object> converter)
        {
            return Cache.GetOrAdd(id, x => new PropertyId(id, name, (a, o) => converter(o)));
        }

        public static PropertyId GetOrCreate(int id, string name, Func<AutomationBase, object, object> converter)
        {
            return Cache.GetOrAdd(id, x => new PropertyId(id, name, converter));
        }

        public static bool TryGet(int id, out PropertyId result)
        {
            return Cache.TryGetValue(id, out result);
        }

        public static PropertyId Find(int id)
        {
            if (Cache.TryGetValue(id, out var result))
            {
                return result;
            }

            throw new KeyNotFoundException($"Did not find a property with id: {id}");
        }

        /// <summary>
        /// Returns a condition for this property with the given value
        /// </summary>
        public PropertyCondition CreateCondition(object value)
        {
            return new PropertyCondition(this, value);
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
