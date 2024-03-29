namespace Synercoding.Primitives;

/// <summary>
/// Represents the <see cref="Orientation"/> a value can have.
/// </summary>
public enum Orientation
{
    /// <summary>
    /// Both sides are of the same length.
    /// </summary>
    Square,
    /// <summary>
    /// Portrait orientation; Width is smaller than height.
    /// </summary>
    Portrait,
    /// <summary>
    /// Landscape orientation.
    /// </summary>
    Landscape
}
