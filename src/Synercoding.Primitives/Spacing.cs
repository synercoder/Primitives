using Synercoding.Primitives.Abstract;
using System;
using System.Text.RegularExpressions;

namespace Synercoding.Primitives;

/// <summary>
/// Value type representing spacing in or around an object.
/// </summary>
public readonly struct Spacing : IConvertable<Spacing>, IEquatable<Spacing>
{
    /// <summary>
    /// Constructor for <see cref="Spacing"/>.
    /// </summary>
    /// <param name="all">Value that will be on all sides.</param>
    /// <param name="unit">The unit of the <paramref name="all"/> parameter.</param>
    public Spacing(double all, Unit unit)
        : this(all, all, all, all, unit)
    { }

    /// <summary>
    /// Constructor for <see cref="Spacing"/>.
    /// </summary>
    /// <param name="left">The amount of left spacing.</param>
    /// <param name="top">The amount of top spacing.</param>
    /// <param name="right">The amount of right spacing.</param>
    /// <param name="bottom">The amount of bottom spacing.</param>
    /// <param name="unit">The unit of the <paramref name="unit"/> parameter.</param>
    public Spacing(double left, double top, double right, double bottom, Unit unit)
    {
        Left = new Value(left, unit);
        Top = new Value(top, unit);
        Right = new Value(right, unit);
        Bottom = new Value(bottom, unit);
    }

    /// <summary>
    /// Constructor for <see cref="Spacing"/>.
    /// </summary>
    /// <param name="all">Value that will be on all sides.</param>
    public Spacing(Value all)
        : this(all, all, all, all)
    { }

    /// <summary>
    /// Constructor for <see cref="Spacing"/>.
    /// </summary>
    /// <param name="left">The amount of left spacing.</param>
    /// <param name="top">The amount of top spacing.</param>
    /// <param name="right">The amount of right spacing.</param>
    /// <param name="bottom">The amount of bottom spacing.</param>
    public Spacing(Value left, Value top, Value right, Value bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    /// <summary>
    /// A deconstructor for <see cref="Spacing"/>.
    /// </summary>
    /// <param name="left">Out parameter for the <see cref="Left"/> property.</param>
    /// <param name="top">Out parameter for the <see cref="Top"/> property.</param>
    /// <param name="right">Out parameter for the <see cref="Right"/> property.</param>
    /// <param name="bottom">Out parameter for the <see cref="Bottom"/> property.</param>
    public void Deconstruct(out Value left, out Value top, out Value right, out Value bottom)
    {
        left = Left;
        top = Top;
        right = Right;
        bottom = Bottom;
    }

    /// <summary>
    /// Representing a <see cref="Spacing"/> of 0.
    /// </summary>
    public static Spacing Nothing
        => new Spacing(0, Unit.Inches);

    /// <summary>
    /// The amount of spacing on the left side.
    /// </summary>
    public Value Left { get; }

    /// <summary>
    /// The amount of spacing on the top side.
    /// </summary>
    public Value Top { get; }

    /// <summary>
    /// The amount of spacing on the right side.
    /// </summary>
    public Value Right { get; }

    /// <summary>
    /// The amount of spacing on the bottom side.
    /// </summary>
    public Value Bottom { get; }

    /// <inheritdoc/>
    public Spacing ConvertTo(Unit unit)
    {
        return new Spacing(
            left: Left.ConvertTo(unit).Raw,
            top: Top.ConvertTo(unit).Raw,
            right: Right.ConvertTo(unit).Raw,
            bottom: Bottom.ConvertTo(unit).Raw,
            unit);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
        => HashCode.Combine(Left, Top, Right, Bottom);

    /// <inheritdoc/>
    public override bool Equals(object? obj)
        => obj is Spacing unit && Equals(unit);

    /// <inheritdoc/>
    public override string ToString() => $"L: {Left}, T: {Top}, R: {Right}, B: {Bottom}";

    /// <inheritdoc/>
    public bool Equals(Spacing other)
    {
        var a = this;
        var b = other;

        return a.Left == b.Left
            && a.Top == b.Top
            && a.Right == b.Right
            && a.Bottom == b.Bottom;
    }

    /// <summary>
    ///  Returns a value that indicates whether two specified <see cref="Spacing"/> values are equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if left and right are equal; otherwise, false.</returns>
    public static bool operator ==(Spacing left, Spacing right)
        => left.Equals(right);

    /// <summary>
    ///  Returns a value that indicates whether two specified <see cref="Spacing"/> values are not equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if left and right are not equal; otherwise, false.</returns>
    public static bool operator !=(Spacing left, Spacing right)
        => !( left == right );

    /// <summary>
    /// Parse a string into a <see cref="Spacing"/>
    /// </summary>
    /// <param name="s"><see cref="string"/> to be parsed.</param>
    /// <returns>A <see cref="Spacing"/> that was represented by <paramref name="s"/>.</returns>
    /// <exception cref="ArgumentException">Throws if <paramref name="s"/> can not be parsed.</exception>
    public static Spacing Parse(string s)
    {
        if (TryParse(s, out var value))
            return value;

        throw new ArgumentException("Argument can't be parsed.", nameof(s));
    }

    /// <summary>
    /// Try to converts a string representation of a <see cref="Spacing"/> into a <see cref="Spacing"/>.
    /// </summary>
    /// <param name="s"><see cref="string"/> to be parsed.</param>
    /// <param name="spacing">Ref parameter with the parsed <see cref="Spacing"/>.</param>
    /// <returns>A <see cref="bool"/> to indicate if the parsing was successful.</returns>
    public static bool TryParse(string s, out Spacing spacing)
    {
        spacing = default;

        s = s.Trim();

        var match = Regex.Match(s, "^L: ?(.+), ?T: ?(.+), ?R: ?(.+), ?B: ?(.+)$");
        if (match.Success && match.Groups.Count == 5)
        {
            if (Value.TryParse(match.Groups[1].Value, out var left)
                && Value.TryParse(match.Groups[2].Value, out var top)
                && Value.TryParse(match.Groups[3].Value, out var right)
                && Value.TryParse(match.Groups[4].Value, out var bottom))
            {
                spacing = new Spacing(left, top, right, bottom);
                return true;
            }
        }

        return false;
    }
}
