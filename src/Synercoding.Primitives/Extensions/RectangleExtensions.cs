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
    /// <param name="value">The amount to contract of each side of the <see cref="Size"/></param>
    /// <returns>A new contracted <see cref="Rectangle"/>.</returns>
    public static Rectangle Contract(this Rectangle rectangle, Value value)
        => rectangle.Contract(new Spacing(value));

    /// <summary>
    /// Expand a given <see cref="Rectangle"/> (make bigger).
    /// </summary>
    /// <param name="rectangle">The <see cref="Rectangle"/> to expand.</param>
    /// <param name="value">The amount to expand to each side of the <see cref="Size"/></param>
    /// <returns>A new expanded <see cref="Rectangle"/>.</returns>
    public static Rectangle Expand(this Rectangle rectangle, Value value)
        => rectangle.Expand(new Spacing(value));

    /// <summary>
    /// Contract a given <see cref="Rectangle"/> (make smaller).
    /// </summary>
    /// <param name="rectangle">The <see cref="Rectangle"/> to contract.</param>
    /// <param name="spacing">The amount of <see cref="Spacing"/> to contract on the sides.</param>
    /// <returns>A new contracted <see cref="Rectangle"/>.</returns>
    public static Rectangle Contract(this Rectangle rectangle, Spacing spacing)
        => new Rectangle(
            llx: rectangle.LLX + spacing.Left,
            lly: rectangle.LLY + spacing.Bottom,
            urx: rectangle.URX - spacing.Right,
            ury: rectangle.URY - spacing.Top);

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
    /// Get the raw values representing this <see cref="Rectangle"/> in a provided unit.
    /// </summary>
    /// <param name="rectangle">The <see cref="Rectangle"/> to get in raw format.</param>
    /// <param name="unit">The unit to get the result in.</param>
    /// <returns>A tuple representing the raw values.</returns>
    public static (double LLX, double LLY, double URX, double URY) AsRaw(this Rectangle rectangle, Unit unit)
        => (rectangle.LLX.AsRaw(unit), rectangle.LLY.AsRaw(unit), rectangle.URX.AsRaw(unit), rectangle.URY.AsRaw(unit));
}
