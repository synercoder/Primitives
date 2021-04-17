namespace Synercoding.Primitives.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Value"/>
    /// </summary>
    public static class ValueExtensions
    {
        /// <summary>
        /// Get a double representing a value in a given unit
        /// </summary>
        /// <param name="value">The value to get the raw number for</param>
        /// <param name="unit">The unit of the returned raw value</param>
        /// <returns>A double representing the value</returns>
        /// <remarks>
        /// Shorthand for <c>value.ConvertTo(unit).Raw</c>
        /// </remarks>
        public static double AsRaw(this Value value, Unit unit)
            => value.ConvertTo(unit).Raw;
    }
}
