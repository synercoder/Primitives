using System.Collections.Generic;
using Xunit;

namespace Synercoding.Primitives.Tests;

public class UnitTests
{
    [Theory]
    [MemberData(nameof(DataFor_TryParse_ReturnsTrue_And_Unit))]
    public void TryParse_ReturnsTrue_And_Unit(string unit, Unit expected)
    {
        // Act
        Unit.TryParse(unit, out var result);

        // Assert
        Assert.Equal(expected, result);
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
            new object[]{ "pts", Unit.Points },
            new object[]{ "points", Unit.Points },
            new object[]{ "dpi(150)", Unit.Pixels(150) },
        };
}
