namespace Synercoding.Primitives.Extensions;

/// <summary>
/// Extension methods for <see cref="Value"/>.
/// </summary>
public static class ValueExtensions
{
    /// <summary>
    /// Get a double representing a <see cref="Value"/> in a given <see cref="Unit"/>.
    /// </summary>
    /// <param name="value">The <see cref="Value"/> to get the raw number for.</param>
    /// <param name="unit">The <see cref="Unit"/> of the returned raw value.</param>
    /// <returns>A double representing the <see cref="Value"/>.</returns>
    /// <remarks>
    /// Shorthand for <c>value.ConvertTo(unit).Raw</c>.
    /// </remarks>
    public static double AsRaw(this Value value, Unit unit)
        => value.ConvertTo(unit).Raw;

    /// <summary>
    /// Get a float representing a <see cref="Value"/> in a given <see cref="Unit"/>.
    /// </summary>
    /// <param name="value">The <see cref="Value"/> to get the raw number for.</param>
    /// <param name="unit">The <see cref="Unit"/> of the returned raw value.</param>
    /// <returns>A float representing the <see cref="Value"/>.</returns>
    /// <remarks>
    /// Shorthand for <c>(float)value.ConvertTo(unit).Raw</c>.
    /// </remarks>
    public static float AsRawF(this Value value, Unit unit)
        => (float)value.ConvertTo(unit).Raw;
}
