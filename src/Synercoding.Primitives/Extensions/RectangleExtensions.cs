namespace Synercoding.Primitives.Extensions;

/// <summary>
/// Extension class for <seealso cref="Rectangle"/>.
/// </summary>
public static class RectangleExtensions
{
    /// <summary>
    /// Contract a given <see cref="Rectangle"/> (make smaller).
    /// </summary>
    /// <param name="rectangle">The <see cref="Rectangle"/> to contract.</param>
    /// <param name="spacing">The amount of <see cref="Spacing"/> to contract on the sides.</param>
    /// <returns>A new contacted <see cref="Rectangle"/>.</returns>
    public static Rectangle Contract(this Rectangle rectangle, Spacing spacing)
        => new Rectangle(
            llx: rectangle.LLX + spacing.Left,
            lly: rectangle.LLY + spacing.Bottom,
            urx: rectangle.URX - spacing.Right,
            ury: rectangle.URY - spacing.Top);

    /// <summary>
    /// Contract a given <see cref="Rectangle"/> (make smaller).
    /// </summary>
    /// <param name="rectangle">The <see cref="Rectangle"/> to contract.</param>
    /// <param name="value">The amount of <see cref="Value"/> to contract on all sides.</param>
    /// <returns>A new contacted <see cref="Rectangle"/>.</returns>
    public static Rectangle Contract(this Rectangle rectangle, Value value)
        => new Rectangle(
            llx: rectangle.LLX + value,
            lly: rectangle.LLY + value,
            urx: rectangle.URX - value,
            ury: rectangle.URY - value);

    /// <summary>
    /// Expand a given <see cref="Rectangle"/> (make bigger).
    /// </summary>
    /// <param name="rectangle">The rectangle to expand</param>
    /// <param name="spacing">The amount of <see cref="Spacing"/> to expand on the sides.</param>
    /// <returns>A new expanded <see cref="Rectangle"/>.</returns>
    public static Rectangle Expand(this Rectangle rectangle, Spacing spacing)
        => new Rectangle(
            llx: rectangle.LLX - spacing.Left,
            lly: rectangle.LLY - spacing.Bottom,
            urx: rectangle.URX + spacing.Right,
            ury: rectangle.URY + spacing.Top);

    /// <summary>
    /// Expand a given <see cref="Rectangle"/> (make bigger).
    /// </summary>
    /// <param name="rectangle">The rectangle to expand</param>
    /// <param name="value">The amount of <see cref="Value"/> to expand on all sides.</param>
    /// <returns>A new expanded <see cref="Rectangle"/>.</returns>
    public static Rectangle Expand(this Rectangle rectangle, Value value)
        => new Rectangle(
            llx: rectangle.LLX - value,
            lly: rectangle.LLY - value,
            urx: rectangle.URX + value,
            ury: rectangle.URY + value);

    /// <summary>
    /// Get the raw values representing this <see cref="Rectangle"/> in a provided unit.
    /// </summary>
    /// <param name="rectangle">The <see cref="Rectangle"/> to get in raw format.</param>
    /// <param name="unit">The unit to get the result in.</param>
    /// <returns>A tuple representing the raw values.</returns>
    public static (double LLX, double LLY, double URX, double URY) AsRaw(this Rectangle rectangle, Unit unit)
        => (rectangle.LLX.AsRaw(unit), rectangle.LLY.AsRaw(unit), rectangle.URX.AsRaw(unit), rectangle.URY.AsRaw(unit));
}
