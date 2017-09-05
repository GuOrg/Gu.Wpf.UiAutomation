namespace Gu.Wpf.UiAutomation
{
    /// <summary>
    /// Inferface for property objects.
    /// </summary>
    /// <typeparam name="T">The type of the value of the property.</typeparam>
    public interface IAutomationProperty<T>
    {
        /// <summary>
        /// Get the value of the property. Throws if the property is not supported or
        /// if it is accessed in a caching context and it is not cached.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Gets a flag if the property is supported or not.
        /// </summary>
        bool IsSupported { get; }

        /// <summary>
        /// Gets the value of the property or the default for this property type if it is not supported.
        /// Throws if the property is accessed in a caching context and it is not cached.
        /// </summary>
        T ValueOrDefault(T @default);

        /// <summary>
        /// Tries to get the value of the property.
        /// Throws if the property is accessed in a caching context and it is not cached.
        /// </summary>
        /// <param name="value">The value of the property. Contains the default if it is not supported.</param>
        /// <returns>True if the property is supported, false otherwise.</returns>
        bool TryGetValue(out T value);
    }
}