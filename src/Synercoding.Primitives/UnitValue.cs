using Synercoding.Primitives.Abstract;
using Synercoding.Primitives.Extensions;
using System;

namespace Synercoding.Primitives
{
    /// <summary>
    /// Represents a double with a unit type attached
    /// </summary>
    /// <example>3mm or 2 inches</example>
    public readonly struct UnitValue : IConvertable<UnitValue>, IComparable, IComparable<UnitValue>, IEquatable<UnitValue>
    {
        private const int ROUND_DIGITS = 15;

        /// <summary>
        /// Constructor for <see cref="UnitValue"/>
        /// </summary>
        /// <param name="value">The number part of the <see cref="UnitValue"/></param>
        /// <param name="unit">The unit part of the <see cref="UnitValue"/></param>
        public UnitValue(double value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// The number part of the <see cref="UnitValue"/>
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// The unit part of the <see cref="UnitValue"/>
        /// </summary>
        public Unit Unit { get; }

        /// <inheritdoc />
        public UnitValue ConvertTo(Unit unit)
        {
            if (Unit == unit)
                return new UnitValue(Value, Unit);

            if (unit == Unit.Inches)
                return new UnitValue(Math.Round(Value / Unit.PerInch, ROUND_DIGITS), Unit.Inches);

            var value = Math.Round(Value / Unit.PerInch * unit.PerInch, ROUND_DIGITS);

            return new UnitValue(value, unit);
        }

        /// <inheritdoc/>
        public int CompareTo(object? obj)
        {
            if (obj is UnitValue unitValue)
                return CompareTo(unitValue);

            throw new ArgumentException($"The provided argument must be a {nameof(UnitValue)}.", nameof(obj));
        }

        /// <inheritdoc/>
        public int CompareTo(UnitValue other)
        {
            var aInches = ConvertTo(Unit.Inches);
            var bInInches = other.ConvertTo(Unit.Inches);

            return aInches.Value.CompareTo(bInInches.Value);
        }

        /// <inheritdoc/>
        public override int GetHashCode() 
            => HashCode.Combine(Value, Unit);

        /// <inheritdoc/>
        public override string ToString() 
            => $"{Value} {Unit.Designation.Shortform()}";

        /// <inheritdoc/>
        public override bool Equals(object? obj)
            => obj is UnitValue unit && Equals(unit);

        /// <inheritdoc/>
        public bool Equals(UnitValue other) 
            => CompareTo(other) == 0;

        /// <summary>
        ///  Returns a value that indicates whether two specified <see cref="UnitValue"/> values are equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if left and right are equal; otherwise, false.</returns>
        public static bool operator ==(UnitValue left, UnitValue right) 
            => left.Equals(right);

        /// <summary>
        ///  Returns a value that indicates whether two specified <see cref="UnitValue"/> values are not equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if left and right are not equal; otherwise, false.</returns>
        public static bool operator !=(UnitValue left, UnitValue right) 
            => !(left == right);

        /// <summary>
        /// Returns a value that indicates whether a specified <see cref="UnitValue"/> value is less than another specified <see cref="UnitValue"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if left is less than right; otherwise, false.</returns>
        public static bool operator <(UnitValue left, UnitValue right) 
            => left.CompareTo(right) < 0;

        /// <summary>
        /// Returns a value that indicates whether a specified <see cref="UnitValue"/> value is greater than another specified <see cref="UnitValue"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if left is greater than right; otherwise, false.</returns>
        public static bool operator >(UnitValue left, UnitValue right) 
            => left.CompareTo(right) > 0;

        /// <summary>
        /// Returns a value that indicates whether a specified <see cref="UnitValue"/> value is less than or equal to another specified <see cref="UnitValue"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if left is less than or equal to right; otherwise, false.</returns>
        public static bool operator <=(UnitValue left, UnitValue right) 
            => left.CompareTo(right) <= 0;
         
        /// <summary>
        /// Returns a value that indicates whether a specified <see cref="UnitValue"/> value is greater than or equal to another specified <see cref="UnitValue"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if left is greater than or equal to right; otherwise, false.</returns>
        public static bool operator >=(UnitValue left, UnitValue right) 
            => left.CompareTo(right) >= 0;

        /// <summary>
        /// Increment the <see cref="UnitValue"/> with one
        /// </summary>
        /// <param name="a">The unit value to increase</param>
        /// <returns>A new <see cref="UnitValue"/> with the new value</returns>
        public static UnitValue operator ++(UnitValue a)
            => new UnitValue(a.Value + 1, a.Unit);

        /// <summary>
        /// Decrement the <see cref="UnitValue"/> with one
        /// </summary>
        /// <param name="a">The unit value to decrement</param>
        /// <returns>A new <see cref="UnitValue"/> with the new value</returns>
        public static UnitValue operator --(UnitValue a)
            => new UnitValue(a.Value - 1, a.Unit);

        /// <summary>
        /// Returns the value of the <see cref="UnitValue"/>. 
        /// </summary>
        /// <param name="a">The <see cref="UnitValue"/> to return</param>
        /// <returns>The <see cref="UnitValue"/></returns>
        public static UnitValue operator +(UnitValue a) 
            => a;

        /// <summary>
        /// Negates a given value
        /// </summary>
        /// <param name="a">The <see cref="UnitValue"/> that will be negated</param>
        /// <returns>The negated value of <paramref name="a"/></returns>
        public static UnitValue operator -(UnitValue a) 
            => new UnitValue(-a.Value, a.Unit);

        /// <summary>
        /// Add two <see cref="UnitValue"/>s together
        /// </summary>
        /// <param name="a">The left side of the plus operation</param>
        /// <param name="b">The right side of the plus operation</param>
        /// <returns>The result of the plus operation</returns>
        public static UnitValue operator +(UnitValue a, UnitValue b)
            => new UnitValue(a.Value + b.ConvertTo(a.Unit).Value, a.Unit);

        /// <summary>
        /// Add a <see cref="double"/> value to a <see cref="UnitValue"/>
        /// </summary>
        /// <param name="a">The left side of the plus operation</param>
        /// <param name="b">The right side of the plus operation</param>
        /// <returns>The result of the plus operation</returns>
        public static UnitValue operator +(UnitValue a, double b)
            => new UnitValue(a.Value + b, a.Unit);

        /// <summary>
        /// Removes a <see cref="UnitValue"/> from another <see cref="UnitValue"/>
        /// </summary>
        /// <param name="a">The left side of the minus operation</param>
        /// <param name="b">The right side of the minus operation</param>
        /// <returns>The result of the minus operation</returns>
        public static UnitValue operator -(UnitValue a, UnitValue b)
            => a + (-b);

        /// <summary>
        /// Removes a <see cref="double"/> from a <see cref="UnitValue"/>
        /// </summary>
        /// <param name="a">The left side of the minus operation</param>
        /// <param name="b">The right side of the minus operation</param>
        /// <returns>The result of the minus operation</returns>
        public static UnitValue operator -(UnitValue a, double b)
            => a + (-b);

        /// <summary>
        /// Multiply a given <see cref="UnitValue"/> by a <see cref="double"/>
        /// </summary>
        /// <param name="a">The <see cref="UnitValue"/> to multiply</param>
        /// <param name="b">The <see cref="double"/> to multiply by</param>
        /// <returns>The result of the multiplication operation</returns>
        public static UnitValue operator *(UnitValue a, double b)
            => new UnitValue(a.Value * b, a.Unit);

        /// <summary>
        /// Divide a given <see cref="UnitValue"/> by another <see cref="UnitValue"/>
        /// </summary>
        /// <param name="a">The left side of the division operation</param>
        /// <param name="b">The right side of the division operation</param>
        /// <returns>The result of the division operation</returns>
        public static double operator /(UnitValue a, UnitValue b)
            => a.Value / b.ConvertTo(a.Unit).Value;

        /// <summary>
        /// Divide a given <see cref="UnitValue"/> by a <see cref="double"/>
        /// </summary>
        /// <param name="a">The <see cref="UnitValue"/> to divide</param>
        /// <param name="b">The <see cref="double"/> to divisor</param>
        /// <returns>The result of the division operation</returns>
        public static double operator /(UnitValue a, double b)
            => a.Value / b;

        /// <summary>
        /// Convert the <see cref="UnitValue"/> to a <see cref="double"/>, losing the <see cref="Unit"/> in the process.
        /// </summary>
        /// <param name="value">The <see cref="double"/> value of this <see cref="UnitValue"/></param>
        public static implicit operator double(UnitValue value) 
            => value.Value;
    }
}
