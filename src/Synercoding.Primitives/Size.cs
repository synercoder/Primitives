using Synercoding.Primitives.Abstract;
using System;
using System.Text.RegularExpressions;

namespace Synercoding.Primitives;

/// <summary>
/// A value type representing a size using <see cref="Width"/> and <see cref="Height"/>.
/// </summary>
public readonly struct Size : IConvertable<Size>, IEquatable<Size>
{
    /// <summary>
    /// Constructor for a <see cref="Size"/>.
    /// </summary>
    /// <param name="width">The width parameter.</param>
    /// <param name="height">The height parameter.</param>
    /// <param name="unit">The <see cref="Unit"/> of the <see cref="Size"/>.</param>
    public Size(double width, double height, Unit unit)
    {
        Width = new Value(width, unit);
        Height = new Value(height, unit);
    }

    /// <summary>
    /// Constructor for a <see cref="Size"/>.
    /// </summary>
    /// <param name="width">The width parameter.</param>
    /// <param name="height">The height parameter.</param>
    public Size(Value width, Value height)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Deconstructor for a <see cref="Size"/>.
    /// </summary>
    /// <param name="width">Out parameter for the <see cref="Width"/> property.</param>
    /// <param name="height">Out parameter for the <see cref="Height"/> property.</param>
    public void Deconstruct(out Value width, out Value height)
    {
        width = Width;
        height = Height;
    }

    /// <summary>
    /// Represents a <see cref="Size"/> with <see cref="Width"/> and <see cref="Height"/> of 0.
    /// </summary>
    public static Size Empty
        => new Size(0, 0, Unit.Inches);

    /// <summary>
    /// The width property.
    /// </summary>
    public Value Width { get; }

    /// <summary>
    /// The height property.
    /// </summary>
    public Value Height { get; }

    /// <summary>
    /// The rotated version of this <see cref="Size"/>.
    /// </summary>
    /// <remarks>
    /// Same as calling <c>var newSize = new Size(oldSize.Height, oldSize.Width);</c>.
    /// </remarks>
    public Size Rotated
        => new Size(Height, Width);

    /// <summary>
    /// Get the <see cref="Primitives.Orientation"/> of the size.
    /// </summary>
    public Orientation Orientation
        => (Width, Height) switch
        {
            var (w, h) when w < h => Orientation.Portrait,
            var (w, h) when w > h => Orientation.Landscape,
            _ => Orientation.Square
        };

    /// <inheritdoc/>
    public Size ConvertTo(Unit unit)
    {
        return new Size(
            width: Width.ConvertTo(unit).Raw,
            height: Height.ConvertTo(unit).Raw,
            unit);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
        => HashCode.Combine(Width, Height);

    /// <inheritdoc/>
    public override bool Equals(object? obj)
        => obj is Size unit && Equals(unit);

    /// <inheritdoc/>
    public bool Equals(Size other)
    {
        var a = this;
        var b = other;

        return a.Width == b.Width
            && a.Height == b.Height;
    }

    /// <inheritdoc/>
    public override string ToString()
        => $"W: {Width}, H: {Height}";

    /// <summary>
    ///  Returns a value that indicates whether two specified <see cref="Size"/> values are equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if left and right are equal; otherwise, false.</returns>
    public static bool operator ==(Size left, Size right)
        => left.Equals(right);

    /// <summary>
    ///  Returns a value that indicates whether two specified <see cref="Size"/> values are not equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if left and right are not equal; otherwise, false.</returns>
    public static bool operator !=(Size left, Size right)
        => !( left == right );

    /// <summary>
    /// Parse a string into a <see cref="Size"/>
    /// </summary>
    /// <param name="s"><see cref="string"/> to be parsed.</param>
    /// <returns>A <see cref="Size"/> that was represented by <paramref name="s"/>.</returns>
    /// <exception cref="ArgumentException">Throws if <paramref name="s"/> can not be parsed.</exception>
    public static Size Parse(string s)
    {
        if (TryParse(s, out var value))
            return value;

        throw new ArgumentException("Argument can't be parsed.", nameof(s));
    }

    /// <summary>
    /// Try to converts a string representation of a <see cref="Size"/> into a <see cref="Size"/>.
    /// </summary>
    /// <param name="s"><see cref="string"/> to be parsed.</param>
    /// <param name="size">Ref parameter with the parsed <see cref="Size"/>.</param>
    /// <returns>A <see cref="bool"/> to indicate if the parsing was successful.</returns>
    public static bool TryParse(string s, out Size size)
    {
        size = default;

        s = s.Trim();

        var match = Regex.Match(s, "^W: ?(.+), ?H: ?(.+)$");
        if (match.Success && match.Groups.Count == 3)
        {
            if(Value.TryParse(match.Groups[1].Value, out var width) && Value.TryParse(match.Groups[2].Value, out var height))
            {
                size = new Size(width, height);
                return true;
            }
        }

        return false;
    }
}
