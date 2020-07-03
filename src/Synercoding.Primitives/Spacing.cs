using Synercoding.Primitives.Abstract;
using System;

namespace Synercoding.Primitives
{
    /// <summary>
    /// Value type representing spacing in or around an object
    /// </summary>
    public readonly struct Spacing : IConvertable<Spacing>, IEquatable<Spacing>
    {
        /// <summary>
        /// Constructor for <see cref="Spacing"/>
        /// </summary>
        /// <param name="all">Value that will be on all sides</param>
        /// <param name="unit">The unit of the <paramref name="all"/> parameter.</param>
        public Spacing(double all, Unit unit)
            : this(all, all, all, all, unit)
        { }

        /// <summary>
        /// Constructor for <see cref="Spacing"/>
        /// </summary>
        /// <param name="left">The amount of left spacing</param>
        /// <param name="top">The amount of top spacing</param>
        /// <param name="right">The amount of right spacing</param>
        /// <param name="bottom">The amount of bottom spacing</param>
        /// <param name="unit">The unit of the <paramref name="unit"/> parameter.</param>
        public Spacing(double left, double top, double right, double bottom, Unit unit)
        {
            Left = new Value(left, unit);
            Top = new Value(top, unit);
            Right = new Value(right, unit);
            Bottom = new Value(bottom, unit);
        }

        /// <summary>
        /// Constructor for <see cref="Spacing"/>
        /// </summary>
        /// <param name="left">The amount of left spacing</param>
        /// <param name="top">The amount of top spacing</param>
        /// <param name="right">The amount of right spacing</param>
        /// <param name="bottom">The amount of bottom spacing</param>
        public Spacing(Value left, Value top, Value right, Value bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// A deconstructor for <see cref="Spacing"/>
        /// </summary>
        /// <param name="left">Out parameter for the <see cref="Left"/> property</param>
        /// <param name="top">Out parameter for the <see cref="Top"/> property</param>
        /// <param name="right">Out parameter for the <see cref="Right"/> property</param>
        /// <param name="bottom">Out parameter for the <see cref="Bottom"/> property</param>
        public void Deconstruct(out Value left, out Value top, out Value right, out Value bottom)
        {
            left = Left;
            top = Top;
            right = Right;
            bottom = Bottom;
        }

        /// <summary>
        /// Representing a spacing of 0
        /// </summary>
        public static Spacing Nothing
            => new Spacing(0, Unit.Inches);

        /// <summary>
        /// The amount of spacing on the left side
        /// </summary>
        public Value Left { get; }

        /// <summary>
        /// The amount of spacing on the top side
        /// </summary>
        public Value Top { get; }

        /// <summary>
        /// The amount of spacing on the right side
        /// </summary>
        public Value Right { get; }

        /// <summary>
        /// The amount of spacing on the bottom side
        /// </summary>
        public Value Bottom { get; }

        /// <inheritdoc/>
        public Spacing ConvertTo(Unit unit)
        {
            return new Spacing(
                left: Left.ConvertTo(unit),
                top: Top.ConvertTo(unit),
                right: Right.ConvertTo(unit),
                bottom: Bottom.ConvertTo(unit),
                unit);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
            => HashCode.Combine(Left, Top, Right, Bottom);

        /// <inheritdoc/>
        public override bool Equals(object? obj)
            => obj is Spacing unit && Equals(unit);

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
    }
}
