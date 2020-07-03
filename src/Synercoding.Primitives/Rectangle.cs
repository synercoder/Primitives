using Synercoding.Primitives.Abstract;
using System;

namespace Synercoding.Primitives
{
    /// <summary>
    /// Value type representing a rectangle
    /// </summary>
    public readonly struct Rectangle : IConvertable<Rectangle>, IEquatable<Rectangle>
    {
        /// <summary>
        /// Constructor for <see cref="Rectangle"/>
        /// </summary>
        /// <param name="llx">The lower left x coordinate</param>
        /// <param name="lly">The lower left y coordinate</param>
        /// <param name="urx">The upper right x coordinate</param>
        /// <param name="ury">The upper right y coordinate</param>
        /// <param name="unit">The unit type of the coordinates</param>
        public Rectangle(double llx, double lly, double urx, double ury, Unit unit)
        {
            LLX = new Value(llx, unit);
            LLY = new Value(lly, unit);
            URX = new Value(urx, unit);
            URY = new Value(ury, unit);
        }

        /// <summary>
        /// Constructor for <see cref="Rectangle"/>
        /// </summary>
        /// <param name="llx">The lower left x coordinate</param>
        /// <param name="lly">The lower left y coordinate</param>
        /// <param name="urx">The upper right x coordinate</param>
        /// <param name="ury">The upper right y coordinate</param>
        public Rectangle(Value llx, Value lly, Value urx, Value ury)
        {
            LLX = llx;
            LLY = lly;
            URX = urx;
            URY = ury;
        }

        /// <summary>
        /// Constructor for <see cref="Rectangle"/>
        /// </summary>
        /// <param name="location">The location of the <see cref="Rectangle"/></param>
        /// <param name="size">The size of the <see cref="Rectangle"/></param>
        public Rectangle(Point location, Size size)
        {
            LLX = location.X;
            LLY = location.Y;
            URX = location.X + size.Width;
            URY = location.Y + size.Height;
        }

        /// <summary>
        /// Deconstruct into a <see cref="Point"/> and <see cref="Size"/>
        /// </summary>
        /// <param name="location">The out parameter for the <see cref="Location"/> property</param>
        /// <param name="size">The out parameter for the <see cref="Size"/> property</param>
        public void Deconstruct(out Point location, out Size size)
        {
            location = Location;
            size = Size;
        }

        /// <summary>
        /// Deconstruct into the coordinate values
        /// </summary>
        /// <param name="llx">The out parameter for the lower left x coordinate</param>
        /// <param name="lly">The out parameter for the lower left y coordinate</param>
        /// <param name="urx">The out parameter for the upper right x coordinate</param>
        /// <param name="ury">The out parameter for the upper right y coordinate</param>
        public void Deconstruct(out Value llx, out Value lly, out Value urx, out Value ury)
        {
            llx = LLX;
            lly = LLY;
            urx = URX;
            ury = URY;
        }

        /// <summary>
        /// Representing a rectangle of Point 0,0 and Size 0,0
        /// </summary>
        public static Rectangle Zero
            => new Rectangle(0, 0, 0, 0, Unit.Inches);

        /// <summary>
        /// The lower left x coordinate
        /// </summary>
        public Value LLX { get; }

        /// <summary>
        /// The lower left y coordinate
        /// </summary>
        public Value LLY { get; }

        /// <summary>
        /// The upper right x coordinate
        /// </summary>
        public Value URX { get; }

        /// <summary>
        /// The upper right y coordinate
        /// </summary>
        public Value URY { get; }

        /// <summary>
        /// The width of this <see cref="Rectangle"/>
        /// </summary>
        public Value Width
            => URX - LLX;

        /// <summary>
        /// The height of this <see cref="Rectangle"/>
        /// </summary>
        public Value Height
            => URY - LLY;

        /// <summary>
        /// The location of this <see cref="Rectangle"/>
        /// </summary>
        public Point Location
            => new Point(LLX, LLY);

        /// <summary>
        /// The size of this <see cref="Rectangle"/>
        /// </summary>
        public Size Size
            => new Size(Width, Height);

        /// <inheritdoc/>
        public Rectangle ConvertTo(Unit unit)
        {
            return new Rectangle(
                llx: LLX.ConvertTo(unit),
                lly: LLY.ConvertTo(unit),
                urx: URX.ConvertTo(unit),
                ury: URY.ConvertTo(unit));
        }

        /// <inheritdoc/>
        public override int GetHashCode()
            => HashCode.Combine(LLX, LLY, URX, URY);

        /// <inheritdoc/>
        public override bool Equals(object? obj)
            => obj is Rectangle unit && Equals(unit);

        /// <inheritdoc/>
        public bool Equals(Rectangle other)
        {
            var a = this;
            var b = other;

            return a.LLX == b.LLX
                && a.LLY == b.LLY
                && a.URX == b.URX
                && a.URY == b.URY;
        }

        /// <inheritdoc/>
        public override string ToString()
            => $"LLX {LLX}, LLY {LLY}, URX {URX}, URY {URY}";

        /// <summary>
        ///  Returns a value that indicates whether two specified <see cref="Rectangle"/> values are equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if left and right are equal; otherwise, false.</returns>
        public static bool operator ==(Rectangle left, Rectangle right)
            => left.Equals(right);

        /// <summary>
        ///  Returns a value that indicates whether two specified <see cref="Rectangle"/> values are not equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if left and right are not equal; otherwise, false.</returns>
        public static bool operator !=(Rectangle left, Rectangle right)
            => !( left == right );
    }
}
