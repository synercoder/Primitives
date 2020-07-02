using Synercoding.Primitives.Abstract;
using System;

namespace Synercoding.Primitives
{
    /// <summary>
    /// A value type representing a point in a 2D space
    /// </summary>
    public readonly struct Point : IConvertable<Point>, IEquatable<Point>
    {
        /// <summary>
        /// Constructor for a <see cref="Point"/>
        /// </summary>
        /// <param name="x">The X coordinate</param>
        /// <param name="y">The Y coordinate</param>
        /// <param name="unit">The unit type of the coordinates</param>
        public Point(double x, double y, Unit unit)
        {
            X = new UnitValue(x, unit);
            Y = new UnitValue(y, unit);
        }

        /// <summary>
        /// Constructor for a <see cref="Point"/>
        /// </summary>
        /// <param name="x">The X coordinate</param>
        /// <param name="y">The Y coordinate</param>
        public Point(UnitValue x, UnitValue y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// A deconstructor for <see cref="Point"/>
        /// </summary>
        /// <param name="x">Out parameter for the <see cref="X"/> property</param>
        /// <param name="y">Out parameter for the <see cref="Y"/> property</param>
        public void Deconstruct(out UnitValue x, out UnitValue y)
        {
            x = X;
            y = Y;
        }

        /// <summary>
        /// The origin point with a default coordinate of 0, 0
        /// </summary>
        public static Point Origin
            => new Point(0, 0, Unit.Inches);

        /// <summary>
        /// The X coordinate
        /// </summary>
        public UnitValue X { get; }

        /// <summary>
        /// The Y coordinate
        /// </summary>
        public UnitValue Y { get; }

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
        public override bool Equals(object obj)
        {
            return obj is Point unit && Equals(unit);
        }

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
    }
}
