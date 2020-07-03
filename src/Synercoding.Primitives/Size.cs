using Synercoding.Primitives.Abstract;
using System;

namespace Synercoding.Primitives
{
    /// <summary>
    /// A value type representing a size using Width and Height
    /// </summary>
    public readonly struct Size : IConvertable<Size>, IEquatable<Size>
    {
        /// <summary>
        /// Constructor for a <see cref="Size"/>
        /// </summary>
        /// <param name="width">The width parameter</param>
        /// <param name="height">The height parameter</param>
        /// <param name="unit">The unit of the <see cref="Size"/></param>
        public Size(double width, double height, Unit unit)
        {
            Width = new Value(width, unit);
            Height = new Value(height, unit);
        }

        /// <summary>
        /// Constructor for a <see cref="Size"/>
        /// </summary>
        /// <param name="width">The width parameter</param>
        /// <param name="height">The height parameter</param>
        public Size(Value width, Value height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Deconstructor for a <see cref="Size"/>
        /// </summary>
        /// <param name="width">Out parameter for the <see cref="Width"/> property</param>
        /// <param name="height">Out parameter for the <see cref="Height"/> property</param>
        public void Deconstruct(out Value width, out Value height)
        {
            width = Width;
            height = Height;
        }

        /// <summary>
        /// Represents a <see cref="Size"/> with <see cref="Width"/> and <see cref="Height"/> of 0
        /// </summary>
        public static Size Empty
            => new Size(0, 0, Unit.Inches);

        /// <summary>
        /// The width property
        /// </summary>
        public Value Width { get; }

        /// <summary>
        /// The height property
        /// </summary>
        public Value Height { get; }

        /// <summary>
        /// Get the <see cref="Primitives.Orientation"/> of the size
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
                width: Width.ConvertTo(unit),
                height: Height.ConvertTo(unit),
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
    }
}
