using Synercoding.Primitives.Extensions;
using System.Collections.Generic;
using Xunit;

namespace Synercoding.Primitives.Tests;

public class RectangleTests
{
    [Fact]
    public void Expand_Spacing_ResultsInLargerRectangle()
    {
        // Arrange
        var rectangle = new Rectangle(10, 10, 10, 10, Unit.Millimeters);
        var spacing = new Spacing(3, Unit.Millimeters);
        var expected = new Rectangle(7, 7, 13, 13, Unit.Millimeters);

        // Act
        var result = rectangle.Expand(spacing);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void EqualityOperator_InchAndMillimeters_NotEqual()
    {
        // Arrange
        var mmRectangle = new Rectangle(1, 1, 1, 1, Unit.Millimeters);
        var inRectangle = new Rectangle(1, 1, 1, 1, Unit.Inches);

        // Act
        var result = mmRectangle == inRectangle;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void EqualityOperator_InchAnd254Millimeters_AreEqual()
    {
        // Arrange
        var mmRectangle = new Rectangle(25.4, 25.4, 25.4, 25.4, Unit.Millimeters);
        var inRectangle = new Rectangle(1, 1, 1, 1, Unit.Inches);

        // Act
        var result = mmRectangle == inRectangle;

        // Assert
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(DataForTryParse_With_IsCorrect))]
    public void TryParse_With_IsCorrect(string input, Rectangle value)
    {
        // Act
        _ = Rectangle.TryParse(input, out var result);

        // Assert
        Assert.Equal(value, result);
    }

    public static IEnumerable<object[]> DataForTryParse_With_IsCorrect
        => new[]
        {
            new object[]{ "LLX:10 mm,LLY:11 mm,URX:110 mm,URY:111 mm", new Rectangle(10,11,110,111, Unit.Millimeters) },
        };

    [Theory]
    [MemberData(nameof(DataForToString_TryParse_Same_Value))]
    public void ToString_TryParse_Same_Value(Rectangle input)
    {
        // Act
        var asText = input.ToString();
        var parsed = Rectangle.Parse(asText);

        // Assert
        Assert.Equal(input, parsed);
    }

    public static IEnumerable<object[]> DataForToString_TryParse_Same_Value
        => new[]
        {
            new object[]{ new Rectangle(1.23, 3.21, 12.34, 43.21, Unit.Millimeters) },
        };

    [Theory]
    [MemberData(nameof(DataForConvert_From_Json_IsCorrect))]
    public void Convert_From_Json_IsCorrect(string value, Rectangle expected)
    {
        // Act
        var result = System.Text.Json.JsonSerializer.Deserialize<Rectangle>(value);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> DataForConvert_From_Json_IsCorrect
        => new[]
        {
                new object[]
                {
                    "{ \"LLX\": \"1mm\", \"LLY\": \"2mm\", \"URX\": \"3mm\", \"URY\": \"4mm\" }",
                    new Rectangle(
                        llx: new Value(1, Unit.Millimeters),
                        lly: new Value(2, Unit.Millimeters),
                        urx: new Value(3, Unit.Millimeters),
                        ury: new Value(4, Unit.Millimeters)
                    ),
                },
        };
}
