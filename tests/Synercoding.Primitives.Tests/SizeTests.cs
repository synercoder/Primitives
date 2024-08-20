using System.Collections.Generic;
using Xunit;

namespace Synercoding.Primitives.Tests;

public class SizeTests
{
    [Fact]
    public void EqualityOperator_InchAndMillimeters_NotEqual()
    {
        // Arrange
        var mmSize = new Size(1, 1, Unit.Millimeters);
        var inSize = new Size(1, 1, Unit.Inches);

        // Act
        var result = mmSize == inSize;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void EqualityOperator_InchAnd254Millimeters_AreEqual()
    {
        // Arrange
        var mmSize = new Size(25.4, 25.4, Unit.Millimeters);
        var inSize = new Size(1, 1, Unit.Inches);

        // Act
        var result = mmSize == inSize;

        // Assert
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(DataForTryParse_With_IsCorrect))]
    public void TryParse_With_IsCorrect(string input, Size value)
    {
        // Act
        _ = Size.TryParse(input, out var result);

        // Assert
        Assert.Equal(value, result);
    }

    public static IEnumerable<object[]> DataForTryParse_With_IsCorrect
        => new[]
        {
            new object[]{ "W:11 mm,H:123.14 mm", new Size(11, 123.14, Unit.Millimeters) },
        };

    [Theory]
    [MemberData(nameof(DataForToString_TryParse_Same_Value))]
    public void ToString_TryParse_Same_Value(Size input)
    {
        // Act
        var asText = input.ToString();
        var parsed = Size.Parse(asText);

        // Assert
        Assert.Equal(input, parsed);
    }

    public static IEnumerable<object[]> DataForToString_TryParse_Same_Value
        => new[]
        {
            new object[]{ new Size(1.23, 3.21, Unit.Millimeters) },
        };

    [Theory]
    [MemberData(nameof(DataForConvert_From_Json_IsCorrect))]
    public void Convert_From_Json_IsCorrect(string value, Size expected)
    {
        // Act
        var result = System.Text.Json.JsonSerializer.Deserialize<Size>(value);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> DataForConvert_From_Json_IsCorrect
        => new[]
        {
                new object[]
                {
                    "{ \"Width\": \"10mm\", \"Height\": \"20mm\" }",
                    new Size(
                        width: new Value(10, Unit.Millimeters),
                        height: new Value(20, Unit.Millimeters)
                    ),
                }
        };
}
