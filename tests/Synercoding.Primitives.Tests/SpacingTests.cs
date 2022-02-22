using System.Collections.Generic;
using Xunit;

namespace Synercoding.Primitives.Tests;

public class SpacingTests
{
    [Fact]
    public void EqualityOperator_InchAndMillimeters_NotEqual()
    {
        // Arrange
        var mmSpacing = new Spacing(1, 1, 1, 1, Unit.Millimeters);
        var inSpacing = new Spacing(1, 1, 1, 1, Unit.Inches);

        // Act
        var result = mmSpacing == inSpacing;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void EqualityOperator_InchAnd254Millimeters_AreEqual()
    {
        // Arrange
        var mmSpacing = new Spacing(25.4, 25.4, 25.4, 25.4, Unit.Millimeters);
        var inSpacing = new Spacing(1, 1, 1, 1, Unit.Inches);

        // Act
        var result = mmSpacing == inSpacing;

        // Assert
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(DataForTryParse_With_IsCorrect))]
    public void TryParse_With_IsCorrect(string input, Spacing value)
    {
        // Act
        _ = Spacing.TryParse(input, out var result);

        // Assert
        Assert.Equal(value, result);
    }

    public static IEnumerable<object[]> DataForTryParse_With_IsCorrect
        => new[]
        {
            new object[]{ "L:10 mm,T:11 mm,R:110 mm,B:111 mm", new Spacing(10,11,110,111, Unit.Millimeters) },
        };

    [Theory]
    [MemberData(nameof(DataForToString_TryParse_Same_Value))]
    public void ToString_TryParse_Same_Value(Spacing input)
    {
        // Act
        var asText = input.ToString();
        var parsed = Spacing.Parse(asText);

        // Assert
        Assert.Equal(input, parsed);
    }

    public static IEnumerable<object[]> DataForToString_TryParse_Same_Value
        => new[]
        {
            new object[]{ new Spacing(1.23, 3.21, 12.34, 43.21, Unit.Millimeters) },
        };
}
