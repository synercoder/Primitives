using System;

namespace Synercoding.Primitives.Extensions;

/// <summary>
/// Extensions for <see cref="UnitDesignation"/>.
/// </summary>
public static class UnitDesignationExtensions
{
    internal const string CM = "cm";
    internal const string IN = "in";
    internal const string MM = "mm";
    internal const string PX = "px";
    internal const string PTS = "pts";

    /// <summary>
    /// Get the shortform notation for a <see cref="UnitDesignation"/>.
    /// </summary>
    /// <param name="designation">The designation to get the shortform for.</param>
    /// <returns>The shortform notation for a <see cref="UnitDesignation"/>.</returns>
    public static string Shortform(this UnitDesignation designation)
        => designation switch
        {
            UnitDesignation.Centimeters => CM,
            UnitDesignation.Inches => IN,
            UnitDesignation.Millimeters => MM,
            UnitDesignation.Pixels => PX,
            UnitDesignation.Points => PTS,
            UnitDesignation x => throw new NotImplementedException($"{x} has no shortform.")
        };
}
