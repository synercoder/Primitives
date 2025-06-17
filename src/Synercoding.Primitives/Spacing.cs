using Synercoding.Primitives.Abstract;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Synercoding.Primitives;

/// <summary>
/// Value type representing spacing in or around an object.
/// </summary>
[JsonConverter(typeof(JsonConverters.SpacingJsonConverter))]
public readonly record struct Spacing : IConvertable<Spacing>, IEquatable<Spacing>, IParsable<Spacing>
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
    public Value Left { get; init; }

    /// <summary>
    /// The amount of spacing on the top side.
    /// </summary>
    public Value Top { get; init; }

    /// <summary>
    /// The amount of spacing on the right side.
    /// </summary>
    public Value Right { get; init; }

    /// <summary>
    /// The amount of spacing on the bottom side.
    /// </summary>
    public Value Bottom { get; init; }

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
    public override string ToString()
        => ToString(null);

    /// <summary>
    /// Returns a string representation of the spacing using the specified format provider.
    /// </summary>
    /// <param name="provider">The format provider to use for formatting numeric values.</param>
    /// <returns>A string representation of the spacing.</returns>
    public string ToString(IFormatProvider? provider)
    {
        if (Left == Top && Top == Right && Right == Bottom)
            return $"All: {Left.ToString(provider)}";

        return $"L: {Left.ToString(provider)}, T: {Top.ToString(provider)}, R: {Right.ToString(provider)}, B: {Bottom.ToString(provider)}";
    }

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
    /// Parse a string into a <see cref="Spacing"/>
    /// </summary>
    /// <param name="s"><see cref="string"/> to be parsed.</param>
    /// <returns>A <see cref="Spacing"/> that was represented by <paramref name="s"/>.</returns>
    /// <exception cref="ArgumentException">Throws if <paramref name="s"/> can not be parsed.</exception>
    public static Spacing Parse(string s)
        => Parse(s, null);

    /// <summary>
    /// Try to converts a string representation of a <see cref="Spacing"/> into a <see cref="Spacing"/>.
    /// </summary>
    /// <param name="s"><see cref="string"/> to be parsed.</param>
    /// <param name="result">Ref parameter with the parsed <see cref="Spacing"/>.</param>
    /// <returns>A <see cref="bool"/> to indicate if the parsing was successful.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Spacing result)
        => TryParse(s, null, out result);

    /// <summary>
    /// Parse a string into a <see cref="Spacing"/>
    /// </summary>
    /// <param name="s"><see cref="string"/> to be parsed.</param>
    /// <param name="provider">Format provider used when parsing values.</param>
    /// <returns>A <see cref="Spacing"/> that was represented by <paramref name="s"/>.</returns>
    /// <exception cref="ArgumentException">Throws if <paramref name="s"/> can not be parsed.</exception>
    public static Spacing Parse(string s, IFormatProvider? provider)
    {
        if (TryParse(s, provider, out var value))
            return value;

        throw new ArgumentException("Argument can't be parsed.", nameof(s));
    }

    /// <summary>
    /// Try to converts a string representation of a <see cref="Spacing"/> into a <see cref="Spacing"/>.
    /// </summary>
    /// <param name="s"><see cref="string"/> to be parsed.</param>
    /// <param name="provider">Format provider used when parsing values.</param>
    /// <param name="result">Out parameter with the parsed <see cref="Spacing"/>.</param>
    /// <returns>A <see cref="bool"/> to indicate if the parsing was successful.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Spacing result)
    {
        result = default;

        if (s is null)
            return false;

        s = s.Trim();

        if (s.StartsWith("All:") && Value.TryParse(s.Substring(4).TrimStart(), out var all))
        {
            result = new Spacing(all);
            return true;
        }

        var match = Regex.Match(s, "^L: ?(.+), ?T: ?(.+), ?R: ?(.+), ?B: ?(.+)$");
        if (match.Success && match.Groups.Count == 5)
        {
            if (Value.TryParse(match.Groups[1].Value, provider, out var left)
                && Value.TryParse(match.Groups[2].Value, provider, out var top)
                && Value.TryParse(match.Groups[3].Value, provider, out var right)
                && Value.TryParse(match.Groups[4].Value, provider, out var bottom))
            {
                result = new Spacing(left, top, right, bottom);
                return true;
            }
        }

        return false;
    }
}
