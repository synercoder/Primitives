using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace Synercoding.Primitives.Tests;

public class UnitTests
{
    [Theory]
    [MemberData(nameof(DataFor_TryParse_ReturnsTrue_And_Unit))]
    public void TryParse_ReturnsTrue_And_Unit(string unit, Unit expected)
    {
        var oldCulture = CultureInfo.CurrentCulture;
        try
        {
            // Assume invariant culture as the default culture for this test.
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            // Act
            Unit.TryParse(unit, out var result);

            // Assert
            Assert.Equal(expected, result);
        }finally
        {
            CultureInfo.CurrentCulture = oldCulture;
        }
    }

    public static IEnumerable<object[]> DataFor_TryParse_ReturnsTrue_And_Unit()
        => new[]
        {
            new object[]{ "mm", Unit.Millimeters },
            new object[]{ "millimeter", Unit.Millimeters },
            new object[]{ "millimeters", Unit.Millimeters },
            new object[]{ "cm", Unit.Centimeters },
            new object[]{ "centimeter", Unit.Centimeters },
            new object[]{ "centimeters", Unit.Centimeters },
            new object[]{ "in", Unit.Inches },
            new object[]{ "inch", Unit.Inches },
            new object[]{ "inches", Unit.Inches },
            new object[]{ "\"", Unit.Inches },
            new object[]{ "pts", Unit.Points },
            new object[]{ "points", Unit.Points },
            new object[]{ "dpi(150)", Unit.Pixels(150) },
            new object[]{ "dpi(150.5)", Unit.Pixels(150.5) },
        };

    [Theory]
    [MemberData(nameof(DataFor_TryParse_WithCulture_ReturnsTrue_And_Unit))]
    public void TryParse_WithCulture_ReturnsTrue_And_Unit(string unit, Unit expected, CultureInfo culture)
    {
        // Act
        Unit.TryParse(unit, culture, out var result);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> DataFor_TryParse_WithCulture_ReturnsTrue_And_Unit()
        => new[]
        {
            new object[]{ "dpi(150.5)", Unit.Pixels(150.5), CultureInfo.InvariantCulture },
            new object[]{ "dpi(150.5)", Unit.Pixels(150.5), new CultureInfo("en-US") },
            new object[]{ "dpi(150,5)", Unit.Pixels(150.5), new CultureInfo("de-DE") },
            new object[]{ "dpi(150,5)", Unit.Pixels(150.5), new CultureInfo("fr-FR") },
            new object[]{ "dpi(1234.56)", Unit.Pixels(1234.56), new CultureInfo("en-US") },
            new object[]{ "dpi(1.234,56)", Unit.Pixels(1234.56), new CultureInfo("de-DE") },
            new object[]{ "dpi(1 234,56)", Unit.Pixels(1234.56), new CultureInfo("fr-FR") },
        };
}
