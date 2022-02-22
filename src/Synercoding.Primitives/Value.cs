using Synercoding.Primitives.Abstract;
using Synercoding.Primitives.Extensions;
using System;
using System.Globalization;

namespace Synercoding.Primitives;

/// <summary>
/// Represents a double with a unit type attached.
/// </summary>
/// <example>3mm or 2 inches</example>
public readonly struct Value : IConvertable<Value>, IComparable, IComparable<Value>, IEquatable<Value>
{
    private const int ROUND_DIGITS = 15;

    /// <summary>
    /// Constructor for <see cref="Value"/>.
    /// </summary>
    /// <param name="value">The number part of the <see cref="Value"/>.</param>
    /// <param name="unit">The unit part of the <see cref="Value"/>.</param>
    public Value(double value, Unit unit)
    {
        Raw = value;
        Unit = unit;
    }

    /// <summary>
    /// The number part of the <see cref="Value"/>.
    /// </summary>
    public double Raw { get; }

    /// <summary>
    /// The unit part of the <see cref="Value"/>.
    /// </summary>
    public Unit Unit { get; }

    /// <inheritdoc />
    public Value ConvertTo(Unit unit)
    {
        if (unit.Designation == 0)
            throw new ArgumentException("The provided unit was not initialized.", nameof(unit));

        if (Unit == unit)
            return new Value(Raw, Unit);

        if (unit == Unit.Inches)
            return new Value(Math.Round(Raw / Unit.PerInch, ROUND_DIGITS), Unit.Inches);

        var value = Math.Round(Raw / Unit.PerInch * unit.PerInch, ROUND_DIGITS);

        return new Value(value, unit);
    }

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is Value unitValue)
            return CompareTo(unitValue);

        throw new ArgumentException($"The provided argument must be a {nameof(Value)}.", nameof(obj));
    }

    /// <inheritdoc/>
    public int CompareTo(Value other)
    {
        var aInches = ConvertTo(Unit.Inches);
        var bInInches = other.ConvertTo(Unit.Inches);

        return aInches.Raw.CompareTo(bInInches.Raw);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
        => HashCode.Combine(Raw, Unit);

    /// <inheritdoc/>
    public override string ToString()
        => $"{Raw.ToString(CultureInfo.InvariantCulture)} {Unit.Designation.Shortform()}";

    /// <inheritdoc/>
    public override bool Equals(object? obj)
        => obj is Value unit && Equals(unit);

    /// <inheritdoc/>
    public bool Equals(Value other)
        => CompareTo(other) == 0;

    /// <summary>
    /// Represents a value of zero
    /// </summary>
    public static Value Zero => new Value(0, Unit.Inches);

    /// <summary>
    ///  Returns a value that indicates whether two specified <see cref="Value"/> values are equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if left and right are equal; otherwise, false.</returns>
    public static bool operator ==(Value left, Value right)
        => left.Equals(right);

    /// <summary>
    ///  Returns a value that indicates whether two specified <see cref="Value"/> values are not equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if left and right are not equal; otherwise, false.</returns>
    public static bool operator !=(Value left, Value right)
        => !( left == right );

    /// <summary>
    /// Returns a value that indicates whether a specified <see cref="Value"/> value is less than another specified <see cref="Value"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if left is less than right; otherwise, false.</returns>
    public static bool operator <(Value left, Value right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Returns a value that indicates whether a specified <see cref="Value"/> value is greater than another specified <see cref="Value"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if left is greater than right; otherwise, false.</returns>
    public static bool operator >(Value left, Value right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Returns a value that indicates whether a specified <see cref="Value"/> value is less than or equal to another specified <see cref="Value"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if left is less than or equal to right; otherwise, false.</returns>
    public static bool operator <=(Value left, Value right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Returns a value that indicates whether a specified <see cref="Value"/> value is greater than or equal to another specified <see cref="Value"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if left is greater than or equal to right; otherwise, false.</returns>
    public static bool operator >=(Value left, Value right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Increment the <see cref="Value"/> with one.
    /// </summary>
    /// <param name="a">The unit value to increase.</param>
    /// <returns>A new <see cref="Value"/> with the incremented value.</returns>
    public static Value operator ++(Value a)
        => new Value(a.Raw + 1, a.Unit);

    /// <summary>
    /// Decrement the <see cref="Value"/> with one.
    /// </summary>
    /// <param name="a">The unit value to decrement.</param>
    /// <returns>A new <see cref="Value"/> with the decremented value.</returns>
    public static Value operator --(Value a)
        => new Value(a.Raw - 1, a.Unit);

    /// <summary>
    /// Returns the value of the <see cref="Value"/>. 
    /// </summary>
    /// <param name="a">The <see cref="Value"/> to return.</param>
    /// <returns>The <see cref="Value"/>.</returns>
    public static Value operator +(Value a)
        => a;

    /// <summary>
    /// Negates a given value.
    /// </summary>
    /// <param name="a">The <see cref="Value"/> that will be negated.</param>
    /// <returns>The negated value of <paramref name="a"/>.</returns>
    public static Value operator -(Value a)
        => new Value(-a.Raw, a.Unit);

    /// <summary>
    /// Add two <see cref="Value"/>s together.
    /// </summary>
    /// <param name="a">The left side of the plus operation.</param>
    /// <param name="b">The right side of the plus operation.</param>
    /// <returns>The result of the plus operation.</returns>
    public static Value operator +(Value a, Value b)
        => new Value(a.Raw + b.ConvertTo(a.Unit).Raw, a.Unit);

    /// <summary>
    /// Add a <see cref="double"/> value to a <see cref="Value"/>.
    /// </summary>
    /// <param name="a">The left side of the plus operation.</param>
    /// <param name="b">The right side of the plus operation.</param>
    /// <returns>The result of the plus operation.</returns>
    public static Value operator +(Value a, double b)
        => new Value(a.Raw + b, a.Unit);

    /// <summary>
    /// Removes a <see cref="Value"/> from another <see cref="Value"/>.
    /// </summary>
    /// <param name="a">The left side of the minus operation.</param>
    /// <param name="b">The right side of the minus operation.</param>
    /// <returns>The result of the minus operation.</returns>
    public static Value operator -(Value a, Value b)
        => a + ( -b );

    /// <summary>
    /// Removes a <see cref="double"/> from a <see cref="Value"/>.
    /// </summary>
    /// <param name="a">The left side of the minus operation.</param>
    /// <param name="b">The right side of the minus operation.</param>
    /// <returns>The result of the minus operation.</returns>
    public static Value operator -(Value a, double b)
        => a + ( -b );

    /// <summary>
    /// Multiply a given <see cref="Value"/> by a <see cref="double"/>.
    /// </summary>
    /// <param name="a">The <see cref="Value"/> to multiply.</param>
    /// <param name="b">The <see cref="double"/> to multiply by</param>
    /// <returns>The result of the multiplication operation.</returns>
    public static Value operator *(Value a, double b)
        => new Value(a.Raw * b, a.Unit);

    /// <summary>
    /// Multiply a given <see cref="Value"/> by a <see cref="double"/>.
    /// </summary>
    /// <param name="a">The <see cref="Value"/> to multiply.</param>
    /// <param name="b">The <see cref="double"/> to multiply by.</param>
    /// <returns>The result of the multiplication operation.</returns>
    public static Value operator *(double a, Value b)
        => new Value(b.Raw * a, b.Unit);

    /// <summary>
    /// Divide a given <see cref="Value"/> by another <see cref="Value"/>.
    /// </summary>
    /// <param name="a">The left side of the division operation.</param>
    /// <param name="b">The right side of the division operation.</param>
    /// <returns>The result of the division operation.</returns>
    public static double operator /(Value a, Value b)
        => a.Raw / b.ConvertTo(a.Unit).Raw;

    /// <summary>
    /// Divide a given <see cref="Value"/> by a <see cref="double"/>.
    /// </summary>
    /// <param name="a">The <see cref="Value"/> to divide.</param>
    /// <param name="b">The <see cref="double"/> to divisor.</param>
    /// <returns>The result of the division operation.</returns>
    public static Value operator /(Value a, double b)
        => new Value(a.Raw / b, a.Unit);

    /// <summary>
    /// Parse a string into a <see cref="Value"/>
    /// </summary>
    /// <param name="s"><see cref="string"/> to be parsed.</param>
    /// <returns>A <see cref="Value"/> that was represented by <paramref name="s"/>.</returns>
    /// <exception cref="ArgumentException">Throws if <paramref name="s"/> can not be parsed.</exception>
    public static Value Parse(string s)
    {
        if (TryParse(s, out var value))
            return value;

        throw new ArgumentException("Argument can't be parsed.", nameof(s));
    }

    /// <summary>
    /// Try to converts a string representation of a <see cref="Value"/> into a <see cref="Value"/>.
    /// </summary>
    /// <param name="s"><see cref="string"/> to be parsed.</param>
    /// <param name="value">Ref parameter with the parsed <see cref="Value"/>.</param>
    /// <returns>A <see cref="bool"/> to indicate if the parsing was successful.</returns>
    public static bool TryParse(string s, out Value value)
    {
        value = default;

        s = s.Trim();

        int index = 0;

        while (index < s.Length)
        {
            if (Unit.TryParse(s.Substring(index), out var unit) && double.TryParse(s.Substring(0, index), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var number))
            {
                value = new Value(number, unit);
                return true;
            }
            index++;
        }

        return false;
    }
}
