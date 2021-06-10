namespace Synercoding.Primitives.Extensions
{
    /// <summary>
    /// Extensions methods for <see cref="Point"/>
    /// </summary>
    public static class PointExtensions
    {
        /// <summary>
        /// Get the raw values representing this Point in a provided unit
        /// </summary>
        /// <param name="point">The point to get in raw format</param>
        /// <param name="unit">The unit to get the result in.</param>
        /// <returns>A tuple representing the raw values.</returns>
        public static (double X, double Y) AsRaw(this Point point, Unit unit)
            => (point.X.AsRaw(unit), point.Y.AsRaw(unit));
    }
}
