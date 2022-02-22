using Synercoding.Primitives.Abstract;
using System;
using System.Text.RegularExpressions;

namespace Synercoding.Primitives;

/// <summary>
/// A value type representing a point in a 2D space.
/// </summary>
public readonly struct Point : IConvertable<Point>, IEquatable<Point>
{
    /// <summary>
    /// Constructor for a <see cref="Point"/>.
    /// </summary>
    /// <param name="x">The <see cref="X"/> coordinate.</param>
    /// <param name="y">The <see cref="Y"/> coordinate.</param>
    /// <param name="unit">The <see cref="Unit"/> type of the <paramref name="x"/> and <paramref name="y"/> parameters.</param>
    public Point(double x, double y, Unit unit)
    {
        X = new Value(x, unit);
        Y = new Value(y, unit);
    }

    /// <summary>
    /// Constructor for a <see cref="Point"/>.
    /// </summary>
    /// <param name="x">The <see cref="X"/> coordinate.</param>
    /// <param name="y">The <see cref="Y"/> coordinate.</param>
    public Point(Value x, Value y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// A deconstructor for <see cref="Point"/>.
    /// </summary>
    /// <param name="x">Out parameter for the <see cref="X"/> property.</param>
    /// <param name="y">Out parameter for the <see cref="Y"/> property.</param>
    public void Deconstruct(out Value x, out Value y)
    {
        x = X;
        y = Y;
    }

    /// <summary>
    /// The origin point with a default coordinate of 0, 0.
    /// </summary>
    public static Point Origin
        => new Point(0, 0, Unit.Inches);

    /// <summary>
    /// The X coordinate.
    /// </summary>
    public Value X { get; }

    /// <summary>
    /// The Y coordinate.
    /// </summary>
    public Value Y { get; }

    /// <inheritdoc />
    public Point ConvertTo(Unit unit)
    {
        return new Point(
            x: X.ConvertTo(unit),
            y: Y.ConvertTo(unit));
    }

    /// <inheritdoc />
    public override int GetHashCode()
        => HashCode.Combine(X, Y);

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is Point unit && Equals(unit);

    /// <inheritdoc />
    public bool Equals(Point other)
    {
        var a = this;
        var b = other;

        return a.X == b.X
            && a.Y == b.Y;
    }

    /// <inheritdoc />
    public override string ToString()
        => $"X: {X}, Y: {Y}";

    /// <summary>
    ///  Returns a value that indicates whether two specified <see cref="Point"/> values are equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if left and right are equal; otherwise, false.</returns>
    public static bool operator ==(Point left, Point right)
        => left.Equals(right);

    /// <summary>
    ///  Returns a value that indicates whether two specified <see cref="Point"/> values are not equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if left and right are not equal; otherwise, false.</returns>
    public static bool operator !=(Point left, Point right)
        => !( left == right );

    /// <summary>
    /// Parse a string into a <see cref="Point"/>
    /// </summary>
    /// <param name="s"><see cref="string"/> to be parsed.</param>
    /// <returns>A <see cref="Point"/> that was represented by <paramref name="s"/>.</returns>
    /// <exception cref="ArgumentException">Throws if <paramref name="s"/> can not be parsed.</exception>
    public static Point Parse(string s)
    {
        if (TryParse(s, out var value))
            return value;

        throw new ArgumentException("Argument can't be parsed.", nameof(s));
    }

    /// <summary>
    /// Try to converts a string representation of a <see cref="Point"/> into a <see cref="Point"/>.
    /// </summary>
    /// <param name="s"><see cref="string"/> to be parsed.</param>
    /// <param name="point">Ref parameter with the parsed <see cref="Point"/>.</param>
    /// <returns>A <see cref="bool"/> to indicate if the parsing was successful.</returns>
    public static bool TryParse(string s, out Point point)
    {
        point = default;

        s = s.Trim();

        var match = Regex.Match(s, "^X: ?(.+), ?Y: ?(.+)$");
        if (match.Success && match.Groups.Count == 3)
        {
            if (Value.TryParse(match.Groups[1].Value, out var x) && Value.TryParse(match.Groups[2].Value, out var y))
            {
                point = new Point(x, y);
                return true;
            }
        }

        return false;
    }
}
