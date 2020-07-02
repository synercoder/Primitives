namespace Synercoding.Primitives.Extensions
{
    /// <summary>
    /// Extension class for <seealso cref="Size"/>
    /// </summary>
    public static class SizeExtensions
    {
        /// <summary>
        /// Contract a given size (make smaller)
        /// </summary>
        /// <param name="size">The size to contract</param>
        /// <param name="spacing">The amount of space to contract on the sides</param>
        /// <returns>A new contacted size</returns>
        public static Size Contract(this Size size, Spacing spacing)
            => new Size(
                width: size.Width - spacing.Left - spacing.Right,
                height: size.Height - spacing.Bottom - spacing.Top);

        /// <summary>
        /// Expand a given size (make bigger)
        /// </summary>
        /// <param name="size">The size to expand</param>
        /// <param name="spacing">The amount of space to expand on the sides</param>
        /// <returns>A new expanded size</returns>
        public static Size Expand(this Size size, Spacing spacing)
            => new Size(
                width: size.Width + spacing.Left + spacing.Right,
                height: size.Height + spacing.Bottom + spacing.Top);

        /// <summary>
        /// Get a <see cref="Rectangle"/> with an origin of 0,0 and Width and Height based upon <paramref name="size"/>
        /// </summary>
        /// <param name="size">The size to base the <see cref="Rectangle"/> on</param>
        /// <returns>A new <see cref="Rectangle"/></returns>
        public static Rectangle AsRectangle(this Size size)
            => new Rectangle(new UnitValue(0, size.Width.Unit), new UnitValue(0, size.Height.Unit), size.Width, size.Height);

        /// <summary>
        /// Get a <see cref="Size"/> where the Width and Height are switched
        /// </summary>
        /// <param name="size">The size to base the new size on</param>
        /// <returns>A new size with width and height switched</returns>
        public static Size Rotate(this Size size)
            => new Size(size.Height, size.Width);
    }
}
