namespace Synercoding.Primitives.Extensions;

/// <summary>
/// Extension class for <seealso cref="Size"/>.
/// </summary>
public static class SizeExtensions
{
    /// <summary>
    /// Contract a given <see cref="Size"/> (make smaller).
    /// </summary>
    /// <param name="size">The <see cref="Size"/> to contract.</param>
    /// <param name="value">The amount to contract of each side of the <see cref="Size"/></param>
    /// <returns>A new contracted <see cref="Size"/>.</returns>
    public static Size Contract(this Size size, Value value)
        => size.Contract(new Spacing(value));

    /// <summary>
    /// Expand a given <see cref="Size"/> (make bigger).
    /// </summary>
    /// <param name="size">The <see cref="Size"/> to expand.</param>
    /// <param name="value">The amount to expand to each side of the <see cref="Size"/></param>
    /// <returns>A new expanded <see cref="Size"/>.</returns>
    public static Size Expand(this Size size, Value value)
        => size.Expand(new Spacing(value));

    /// <summary>
    /// Contract a given <see cref="Size"/> (make smaller).
    /// </summary>
    /// <param name="size">The <see cref="Size"/> to contract.</param>
    /// <param name="spacing">The amount of <see cref="Spacing"/> to contract on the sides.</param>
    /// <returns>A new contracted <see cref="Size"/>.</returns>
    public static Size Contract(this Size size, Spacing spacing)
        => new Size(
            width: size.Width - spacing.Left - spacing.Right,
            height: size.Height - spacing.Bottom - spacing.Top);

    /// <summary>
    /// Expand a given <see cref="Size"/> (make bigger).
    /// </summary>
    /// <param name="size">The <see cref="Size"/> to expand.</param>
    /// <param name="spacing">The amount of <see cref="Spacing"/> to expand on the sides.</param>
    /// <returns>A new expanded <see cref="Size"/>.</returns>
    public static Size Expand(this Size size, Spacing spacing)
        => new Size(
            width: size.Width + spacing.Left + spacing.Right,
            height: size.Height + spacing.Bottom + spacing.Top);

    /// <summary>
    /// Get a <see cref="Rectangle"/> with an <see cref="Rectangle.Location"/> of 0,0 and <see cref="Rectangle.Width"/> and <see cref="Rectangle.Height"/> based upon <paramref name="size"/>.
    /// </summary>
    /// <param name="size">The <see cref="Size"/> to base the <see cref="Rectangle"/> on.</param>
    /// <returns>A new <see cref="Rectangle"/>.</returns>
    public static Rectangle AsRectangle(this Size size)
        => new Rectangle(new Value(0, size.Width.Unit), new Value(0, size.Height.Unit), size.Width, size.Height);

    /// <summary>
    /// Get the raw values representing this <see cref="Size"/> in a provided <see cref="Unit"/>.
    /// </summary>
    /// <param name="size">The <see cref="Size"/> to get in raw format.</param>
    /// <param name="unit">The <see cref="Unit"/> to get the result in.</param>
    /// <returns>A tuple representing the raw values.</returns>
    public static (double Width, double Height) AsRaw(this Size size, Unit unit)
        => (size.Width.AsRaw(unit), size.Height.AsRaw(unit));
}
