using Synercoding.Primitives.Extensions;
using System;
using System.Text.RegularExpressions;

namespace Synercoding.Primitives;

/// <summary>
/// Value type representing an <see cref="Unit"/>.
/// </summary>
public readonly struct Unit : IEquatable<Unit>
{
    private Unit(double perInch, UnitDesignation unitDesignation)
    {
        if (perInch <= 0)
            throw new ArgumentOutOfRangeException(nameof(perInch), "The value needs to be a non-zero-non-negative number.");
        if (!Enum.IsDefined(typeof(UnitDesignation), unitDesignation))
            throw new ArgumentOutOfRangeException(nameof(unitDesignation), $"The value needs to be a defined value of {nameof(UnitDesignation)}.");

        PerInch = perInch;
        Designation = unitDesignation;
    }

    /// <summary>
    /// Value representing how many of this <see cref="Unit"/> are in an inch.
    /// </summary>
    public double PerInch { get; }

    /// <summary>
    /// The designation of the unit.
    /// </summary>
    public UnitDesignation Designation { get; }

    /// <inheritdoc/>
    public bool Equals(Unit other)
    {
        var a = this;
        var b = other;

        return a.PerInch == b.PerInch
            && a.Designation == b.Designation;
    }

    /// <inheritdoc/>
    public override string ToString() 
        => $"{Designation.Shortform()} ({PerInch} per inch)";

    /// <inheritdoc/>
    public override bool Equals(object? obj)
        => obj is Unit unit && Equals(unit);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(PerInch, Designation);

    /// <summary>
    ///  Returns a value that indicates whether two specified <see cref="Unit"/> values are equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if left and right are equal; otherwise, false.</returns>
    public static bool operator ==(Unit left, Unit right) => left.Equals(right);

    /// <summary>
    ///  Returns a value that indicates whether two specified <see cref="Unit"/> values are not equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if left and right are not equal; otherwise, false.</returns>
    public static bool operator !=(Unit left, Unit right) => !( left == right );

    /// <summary>
    /// Unit for millimeters.
    /// </summary>
    public static Unit Millimeters { get; } = new Unit(25.4, UnitDesignation.Millimeters);

    /// <summary>
    /// Unit for centimeters.
    /// </summary>
    public static Unit Centimeters { get; } = new Unit(2.54, UnitDesignation.Centimeters);

    /// <summary>
    /// Unit of points.
    /// </summary>
    public static Unit Points { get; } = new Unit(72, UnitDesignation.Points);

    /// <summary>
    /// Unit for inches.
    /// </summary>
    public static Unit Inches { get; } = new Unit(1, UnitDesignation.Inches);

    /// <summary>
    /// Get a <see cref="Unit"/> representing a certain DPI.
    /// </summary>
    /// <param name="dpi">The DPI the <see cref="Unit"/> will represent.</param>
    /// <returns>The <see cref="Unit"/> representing pixels at a certain DPI.</returns>
    public static Unit Pixels(int dpi) => new Unit(dpi, UnitDesignation.Pixels);

    /// <summary>
    /// Get the <see cref="Unit"/> that correspons to the <paramref name="designation"/>.
    /// </summary>
    /// <param name="designation">The <see cref="UnitDesignation"/> to get the <see cref="Unit"/> for.</param>
    /// <returns>The correct <see cref="Unit"/>.</returns>
    /// <exception cref="ArgumentException">Throws when <see cref="UnitDesignation.Pixels"/> is provided. <see cref="UnitDesignation.Pixels"/> can only be used in combination with DPI by using the <see cref="Pixels(int)"/> method.</exception>
    public static Unit FromDesignation(UnitDesignation designation)
        => designation switch
        {
            UnitDesignation.Millimeters => Millimeters,
            UnitDesignation.Centimeters => Centimeters,
            UnitDesignation.Inches => Inches,
            UnitDesignation.Points => Points,
            UnitDesignation.Pixels => throw new ArgumentException($"The pixels designation can only be used in combination with a DPI parameter, use the {nameof(Pixels)} method instead.", nameof(designation)),
            _ => throw new NotImplementedException($"{designation} can not be used to get a Unit type.")
        };

    /// <summary>
    /// Parse a string into a <see cref="Unit"/>
    /// </summary>
    /// <param name="s"><see cref="string"/> to be parsed.</param>
    /// <returns>A <see cref="Unit"/> that was represented by <paramref name="s"/>.</returns>
    /// <exception cref="ArgumentException">Throws if <paramref name="s"/> can not be parsed.</exception>
    public static Unit Parse(string s)
    {
        if (TryParse(s, out var unit))
            return unit;

        throw new ArgumentException("Argument can't be parsed.", nameof(s));
    }

    /// <summary>
    /// Try to converts a string representation of a <see cref="Unit"/> into a <see cref="Unit"/>.
    /// </summary>
    /// <param name="s"><see cref="string"/> to be parsed.</param>
    /// <param name="unit">Ref parameter with the parsed <see cref="Unit"/>.</param>
    /// <returns>A <see cref="bool"/> to indicate if the parsing was successful.</returns>
    public static bool TryParse(string s, out Unit unit)
    {
        s = s.Trim();

        if (Enum.TryParse<UnitDesignation>(s, true, out var designation))
        {
            unit = FromDesignation(designation);
            return true;
        }

        unit = s.ToLowerInvariant() switch
        {
            UnitDesignationExtensions.MM => Millimeters,
            UnitDesignationExtensions.CM => Centimeters,
            UnitDesignationExtensions.IN => Inches,
            UnitDesignationExtensions.PTS => Points,
            "millimeter" => Millimeters,
            "centimeter" => Centimeters,
            "inch" => Inches,
            "point" => Points,
            _ => default
        };

        if(unit == default && s.StartsWith("dpi"))
        {
            var match = Regex.Match(s, "^dpi\\(([0-9]+)\\)$");
            if(match.Success && match.Groups.Count == 2)
            {
                var dpi = int.Parse(match.Groups[1].Value);
                unit = Pixels(dpi);
            }
        }

        return unit != default;
    }
}
