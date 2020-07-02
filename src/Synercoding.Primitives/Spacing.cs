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
            Left = new UnitValue(left, unit);
            Top = new UnitValue(top, unit);
            Right = new UnitValue(right, unit);
            Bottom = new UnitValue(bottom, unit);
        }

        /// <summary>
        /// Constructor for <see cref="Spacing"/>
        /// </summary>
        /// <param name="left">The amount of left spacing</param>
        /// <param name="top">The amount of top spacing</param>
        /// <param name="right">The amount of right spacing</param>
        /// <param name="bottom">The amount of bottom spacing</param>
        public Spacing(UnitValue left, UnitValue top, UnitValue right, UnitValue bottom)
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
        public void Deconstruct(out UnitValue left, out UnitValue top, out UnitValue right, out UnitValue bottom)
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
        public UnitValue Left { get; }

        /// <summary>
        /// The amount of spacing on the top side
        /// </summary>
        public UnitValue Top { get; }

        /// <summary>
        /// The amount of spacing on the right side
        /// </summary>
        public UnitValue Right { get; }

        /// <summary>
        /// The amount of spacing on the bottom side
        /// </summary>
        public UnitValue Bottom { get; }

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
        public override bool Equals(object obj)
        {
            return obj is Spacing unit && Equals(unit);
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
    }
}
