using System.Collections.Generic;
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
    public void TryParse_With_IsCorrect(string input, Point value)
    {
        // Act
        _ = Point.TryParse(input, out var result);

        // Assert
        Assert.Equal(value, result);
    }

    public static IEnumerable<object[]> DataForTryParse_With_IsCorrect
        => new[]
        {
            new object[]{ "X:11 mm,Y:123.14 mm", new Point(11, 123.14, Unit.Millimeters) },
        };

    [Theory]
    [MemberData(nameof(DataForToString_TryParse_Same_Value))]
    public void ToString_TryParse_Same_Value(Point input)
    {
        // Act
        var asText = input.ToString();
        var parsed = Point.Parse(asText);

        // Assert
        Assert.Equal(input, parsed);
    }

    public static IEnumerable<object[]> DataForToString_TryParse_Same_Value
        => new[]
        {
            new object[]{ new Point(1.23, 3.21, Unit.Millimeters) },
        };
}
