namespace Synercoding.Primitives.Extensions
{
    /// <summary>
    /// Extension class for <seealso cref="Rectangle"/>
    /// </summary>
    public static class RectangleExtensions
    {
        /// <summary>
        /// Contract a given rectangle (make smaller)
        /// </summary>
        /// <param name="rectangle">The rectangle to contract</param>
        /// <param name="spacing">The amount of space to contract on the sides</param>
        /// <returns>A new contacted rectangle</returns>
        public static Rectangle Contract(this Rectangle rectangle, Spacing spacing)
            => new Rectangle(
                llx: rectangle.LLX + spacing.Left,
                lly: rectangle.LLY + spacing.Bottom,
                urx: rectangle.URX - spacing.Right,
                ury: rectangle.URY - spacing.Top);

        /// <summary>
        /// Expand a given rectangle (make bigger)
        /// </summary>
        /// <param name="rectangle">The rectangle to expand</param>
        /// <param name="spacing">The amount of space to expand on the sides</param>
        /// <returns>A new expanded rectangle</returns>
        public static Rectangle Expand(this Rectangle rectangle, Spacing spacing)
            => new Rectangle(
                llx: rectangle.LLX - spacing.Left,
                lly: rectangle.LLY - spacing.Bottom,
                urx: rectangle.URX + spacing.Right,
                ury: rectangle.URY + spacing.Top);
    }
}
