namespace Synercoding.Primitives;

/// <summary>
/// Different designations for units
/// </summary>
public enum UnitDesignation : byte
{
    /// <summary>
    /// Points, 72 per inch
    /// </summary>
    Points = 1,
    /// <summary>
    /// Millimeters, 25.4 per inch
    /// </summary>
    Millimeters = 2,
    /// <summary>
    /// Centimeters, 2.54 per inch
    /// </summary>
    Centimeters = 3,
    /// <summary>
    /// Pixels, dependent on DPI
    /// </summary>
    Pixels = 4,
    /// <summary>
    /// Inches
    /// </summary>
    Inches = 5
}