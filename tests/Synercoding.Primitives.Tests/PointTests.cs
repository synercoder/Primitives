using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace Synercoding.Primitives.Tests;

public class PointTests
{

    [Fact]
    public void EqualityOperator_InchAndMillimeters_NotEqual()
    {
        // Arrange
        var mmPoint = new Point(1, 1, Unit.Millimeters);
        var inPoint = new Point(1, 1, Unit.Inches);

        // Act
        var result = mmPoint == inPoint;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void EqualityOperator_InchAnd254Millimeters_AreEqual()
    {
        // Arrange
        var mmPoint = new Point(25.4, 25.4, Unit.Millimeters);
        var inPoint = new Point(1, 1, Unit.Inches);

        // Act
        var result = mmPoint == inPoint;

        // Assert
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(DataForTryParse_With_IsCorrect))]
    public void TryParse_With_IsCorrect(string input, Point value, CultureInfo culture)
    {
        // Act
        _ = Point.TryParse(input, culture, out var result);

        // Assert
        Assert.Equal(value, result);
    }

    public static IEnumerable<object[]> DataForTryParse_With_IsCorrect
        => new[]
        {
            new object[]{ "X:11 mm,Y:123.14 mm", new Point(11, 123.14, Unit.Millimeters), CultureInfo.InvariantCulture },
            new object[]{ "X:1.5 mm,Y:2.75 cm", new Point(new Value(1.5, Unit.Millimeters), new Value(2.75, Unit.Centimeters)), new CultureInfo("en-US") },
            new object[]{ "X:1,5 mm,Y:2,75 cm", new Point(new Value(1.5, Unit.Millimeters), new Value(2.75, Unit.Centimeters)), new CultureInfo("de-DE") },
            new object[]{ "X:1,5 mm,Y:2,75 cm", new Point(new Value(1.5, Unit.Millimeters), new Value(2.75, Unit.Centimeters)), new CultureInfo("fr-FR") },
            new object[]{ "X:1,234.56 in,Y:-789.12 pts", new Point(new Value(1234.56, Unit.Inches), new Value(-789.12, Unit.Points)), new CultureInfo("en-US") },
            new object[]{ "X:1.234,56 in,Y:-789,12 pts", new Point(new Value(1234.56, Unit.Inches), new Value(-789.12, Unit.Points)), new CultureInfo("de-DE") },
        };

    [Theory]
    [MemberData(nameof(DataForToString_TryParse_Same_Value))]
    public void ToString_TryParse_Same_Value(Point input, CultureInfo culture)
    {
        // Act
        var asText = input.ToString(culture);
        var parsed = Point.Parse(asText, culture);

        // Assert
        Assert.Equal(input, parsed);
    }

    public static IEnumerable<object[]> DataForToString_TryParse_Same_Value
        => new[]
        {
            new object[]{ new Point(1.23, 3.21, Unit.Millimeters), CultureInfo.InvariantCulture },
            new object[]{ new Point(1.5, 2.75, Unit.Millimeters), new CultureInfo("en-US") },
            new object[]{ new Point(1.5, 2.75, Unit.Centimeters), new CultureInfo("de-DE") },
            new object[]{ new Point(3.25, 4.5, Unit.Inches), new CultureInfo("fr-FR") },
            new object[]{ new Point(new Value(1234.56, Unit.Millimeters), new Value(-789.12, Unit.Points)), new CultureInfo("en-US") },
            new object[]{ new Point(new Value(1234.56, Unit.Millimeters), new Value(-789.12, Unit.Points)), new CultureInfo("de-DE") },
            new object[]{ new Point(new Value(1234.56, Unit.Millimeters), new Value(-789.12, Unit.Points)), new CultureInfo("fr-FR") },
        };

    [Theory]
    [MemberData(nameof(DataForConvert_From_Json_IsCorrect))]
    public void Convert_From_Json_IsCorrect(string value, Point expected)
    {
        // Act
        var result = System.Text.Json.JsonSerializer.Deserialize<Point>(value);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> DataForConvert_From_Json_IsCorrect
        => new[]
        {
                new object[]
                {
                    "{ \"X\": \"10mm\", \"Y\": \"20mm\" }",
                    new Point(
                        x: new Value(10, Unit.Millimeters),
                        y: new Value(20, Unit.Millimeters)
                    ),
                }
        };
}
