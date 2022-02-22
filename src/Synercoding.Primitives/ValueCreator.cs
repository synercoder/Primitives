namespace Synercoding.Primitives;

/// <summary>
/// Class that can be used as a static using to have quick &amp; easy access to values
/// </summary>
/// <example>
/// using static Synercoding.Primitives.ValueCreator;
/// </example>
public static class ValueCreator
{
    /// <summary>
    /// Create a new <see cref="Value"/> with <see cref="Unit"/> <see cref="Unit.Millimeters"/>
    /// </summary>
    /// <param name="mm">The amount of mm</param>
    /// <returns>A new <see cref="Value"/> object.</returns>
    public static Value Mm(double mm) => new Value(mm, Unit.Millimeters);
    /// <summary>
    /// Create a new <see cref="Value"/> with <see cref="Unit"/> <see cref="Unit.Centimeters"/>
    /// </summary>
    /// <param name="cm">The amount of cm</param>
    /// <returns>A new <see cref="Value"/> object.</returns>
    public static Value Cm(double cm) => new Value(cm, Unit.Centimeters);
    /// <summary>
    /// Create a new <see cref="Value"/> with <see cref="Unit"/> <see cref="Unit.Inches"/>
    /// </summary>
    /// <param name="inch">The amount of inch</param>
    /// <returns>A new <see cref="Value"/> object.</returns>
    public static Value Inch(double inch) => new Value(inch, Unit.Inches);
    /// <summary>
    /// Create a new <see cref="Value"/> with <see cref="Unit"/> <see cref="Unit.Points"/>
    /// </summary>
    /// <param name="pts">The amount of pts</param>
    /// <returns>A new <see cref="Value"/> object.</returns>
    public static Value Pts(double pts) => new Value(pts, Unit.Points);

    /// <summary>
    /// Create a new <see cref="Value"/> with <see cref="Unit"/> <see cref="Unit.Pixels(int)"/>
    /// </summary>
    /// <param name="dots">The amount of pixels</param>
    /// <param name="dpi">The dpi</param>
    /// <returns>A new <see cref="Value"/> object.</returns>
    public static Value Pixels(double dots, int dpi) => new Value(dots, Unit.Pixels(dpi));
}
