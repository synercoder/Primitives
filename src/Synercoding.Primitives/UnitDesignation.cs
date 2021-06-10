namespace Synercoding.Primitives
{
    /// <summary>
    /// Different designations for units
    /// </summary>
    public enum UnitDesignation : byte
    {
        /// <summary>
        /// Points, 72 per inch
        /// </summary>
        Points,
        /// <summary>
        /// Millimeters, 25.4 per inch
        /// </summary>
        Millimeters,
        /// <summary>
        /// Centimeters, 2.54 per inch
        /// </summary>
        Centimeters,
        /// <summary>
        /// Pixels, dependent on DPI
        /// </summary>
        Pixels,
        /// <summary>
        /// Inches
        /// </summary>
        Inches
    }
}