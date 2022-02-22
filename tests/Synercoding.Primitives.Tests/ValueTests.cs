using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Synercoding.Primitives.Tests
{
    public class ValueTests
    {
        [Fact]
        public void LessThanOperator_MillimetersAndInch_IsSmaller()
        {
            // Arrange
            var mm = new Value(1, Unit.Millimeters);
            var inch = new Value(1, Unit.Inches);

            // Act
            var result = mm < inch;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOperator_PointsAndMillimeters_IsSmaller()
        {
            // Arrange
            var mm = new Value(1, Unit.Millimeters);
            var point = new Value(1, Unit.Points);

            // Act
            var result = point < mm;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualsOperator_25dot4MmAndOneInch_AreEqual()
        {
            // Arrange
            var mm = new Value(25.4, Unit.Millimeters);
            var inch = new Value(1, Unit.Inches);

            // Act
            var result = mm == inch;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Convert_OneInchToMillimeters_25dot4Millimeters()
        {
            // Arrange
            var inch = new Value(1, Unit.Inches);

            // Act
            var mm = inch.ConvertTo(Unit.Millimeters);

            // Assert
            Assert.True(mm.Unit == Unit.Millimeters);
            Assert.True(mm.Raw == 25.4);
        }

        [Fact]
        public void EqualsOperator_72pointsAndOneInch_AreEqual()
        {
            // Arrange
            var points = new Value(72, Unit.Points);
            var inch = new Value(1, Unit.Inches);

            // Act
            var result = points == inch;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualsOperator_600pxAt300dpiAndTwoInches_AreEqual()
        {
            // Arrange
            var pixels = new Value(600, Unit.Pixels(300));
            var inch = new Value(2, Unit.Inches);

            // Act
            var result = pixels == inch;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AdditionOperator_MillimetersAndInches_ResultIs26dot4mm()
        {
            // Arrange
            var mm = new Value(1, Unit.Millimeters);
            var inch = new Value(1, Unit.Inches);
            var expected = new Value(26.4, Unit.Millimeters);

            // Act
            var result = mm + inch;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DivisionOperator_1cmAnd1mm_containes10times()
        {
            // Arrange
            var cm = new Value(1, Unit.Centimeters);
            var mm = new Value(1, Unit.Millimeters);
            var expected = 10;

            // Act
            var result = cm / mm;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DivisionOperator_1cmAnd2_Is0dot5cm()
        {
            // Arrange
            var cm = new Value(1, Unit.Centimeters);
            var amount = 2;
            var expected = new Value(0.5, Unit.Centimeters);

            // Act
            var result = cm / amount;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FromDesignation_AllEnumValuesExceptPx_DoesNotThrowNotImplementedException()
        {
            var unitDesignations = Enum.GetValues(typeof(UnitDesignation))
                .OfType<UnitDesignation>()
                .Where(x => x != UnitDesignation.Pixels) // Exclude pixels, only works when providing a DPI
                .ToArray();

            foreach (var designation in unitDesignations)
                _ = Unit.FromDesignation(designation);
        }

        [Fact]
        public void FromDesignation_WithPixels_ThrowsArgumentException()
        {
            var designation = UnitDesignation.Pixels;

            Assert.Throws<ArgumentException>(() => Unit.FromDesignation(designation));
        }

        [Fact]
        public void MultiplicationOperator_WithLeftDouble_IsUnitValue()
        {
            // Arrange
            var left = 2d;
            var right = new Value(5, Unit.Millimeters);
            var expected = new Value(10, Unit.Millimeters);

            // Act
            var result = left * right;

            // Assert
            Assert.Equal(expected, result);
            Assert.Equal(expected.Raw, result.Raw);
            Assert.Equal(expected.Unit, result.Unit);
        }

        [Fact]
        public void MultiplicationOperator_WithRightDouble_IsUnitValue()
        {
            // Arrange
            var left = new Value(5, Unit.Millimeters);
            var right = 2d;
            var expected = new Value(10, Unit.Millimeters);

            // Act
            var result = left * right;

            // Assert
            Assert.Equal(expected, result);
            Assert.Equal(expected.Raw, result.Raw);
            Assert.Equal(expected.Unit, result.Unit);
        }

        [Theory]
        [InlineData("1mm")]
        [InlineData("100cm")]
        [InlineData("100pts")]
        [InlineData("300dpi(100)")]
        [InlineData("100dpi(300)")]
        [InlineData("1 mm")]
        [InlineData("100 cm")]
        [InlineData("100 pts")]
        [InlineData("300 dpi(100)")]
        [InlineData("100 dpi(300)")]
        public void TryParse_With_CanParse(string input)
        {
            // Act
            var result = Value.TryParse(input, out _);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(DataForToString_TryParse_Same_Value))]
        public void ToString_TryParse_Same_Value(Value input)
        {
            // Act
            var asText = input.ToString();
            var parsed = Value.Parse(asText);

            // Assert
            Assert.Equal(input, parsed);
        }

        [Theory]
        [MemberData(nameof(DataForTryParse_With_IsCorrect))]
        public void TryParse_With_IsCorrect(string input, Value value)
        {
            // Act
            _ = Value.TryParse(input, out var result);

            // Assert
            Assert.Equal(value, result);
        }

        public static IEnumerable<object[]> DataForToString_TryParse_Same_Value
            => new[]
            {
                new object[]{ new Value(1, Unit.Millimeters) },
                new object[]{ new Value(1, Unit.Millimeters) },
                new object[]{ new Value(-1, Unit.Millimeters) },
                new object[]{ new Value(-1, Unit.Millimeters) },
                new object[]{ new Value(1.10, Unit.Millimeters) },
                new object[]{ new Value(1.10, Unit.Millimeters) },
                new object[]{ new Value(-1.10, Unit.Millimeters) },
                new object[]{ new Value(-1.10, Unit.Millimeters) },
                new object[]{ new Value(10, Unit.Centimeters) },
                new object[]{ new Value(10, Unit.Centimeters) },
                new object[]{ new Value(-10, Unit.Centimeters) },
                new object[]{ new Value(-10, Unit.Centimeters) },
            };

        public static IEnumerable<object[]> DataForTryParse_With_IsCorrect
            => new[]
            {
                new object[]{ "1mm", new Value(1, Unit.Millimeters) },
                new object[]{ "1 mm", new Value(1, Unit.Millimeters) },
                new object[]{ "-1mm", new Value(-1, Unit.Millimeters) },
                new object[]{ "-1 mm", new Value(-1, Unit.Millimeters) },
                new object[]{ "1.10mm", new Value(1.10, Unit.Millimeters) },
                new object[]{ "1.10 mm", new Value(1.10, Unit.Millimeters) },
                new object[]{ "-1.10mm", new Value(-1.10, Unit.Millimeters) },
                new object[]{ "-1.10 mm", new Value(-1.10, Unit.Millimeters) },
                new object[]{ "1in", new Value(1, Unit.Inches) },
                new object[]{ "1 in", new Value(1, Unit.Inches) },
                new object[]{ "-1in", new Value(-1, Unit.Inches) },
                new object[]{ "-1 in", new Value(-1, Unit.Inches) },
                new object[]{ "10cm", new Value(10, Unit.Centimeters) },
                new object[]{ "10 cm", new Value(10, Unit.Centimeters) },
                new object[]{ "-10cm", new Value(-10, Unit.Centimeters) },
                new object[]{ "-10 cm", new Value(-10, Unit.Centimeters) },
                new object[]{ "10pts", new Value(10, Unit.Points) },
                new object[]{ "10 pts", new Value(10, Unit.Points) },
                new object[]{ "-10pts", new Value(-10, Unit.Points) },
                new object[]{ "-10 pts", new Value(-10, Unit.Points) },

                new object[]{ "1millimeters", new Value(1, Unit.Millimeters) },
                new object[]{ "1 millimeters", new Value(1, Unit.Millimeters) },
                new object[]{ "-1millimeters", new Value(-1, Unit.Millimeters) },
                new object[]{ "-1 millimeters", new Value(-1, Unit.Millimeters) },
                new object[]{ "1inches", new Value(1, Unit.Inches) },
                new object[]{ "1 inches", new Value(1, Unit.Inches) },
                new object[]{ "-1inches", new Value(-1, Unit.Inches) },
                new object[]{ "-1 inches", new Value(-1, Unit.Inches) },
                new object[]{ "1inch", new Value(1, Unit.Inches) },
                new object[]{ "1 inch", new Value(1, Unit.Inches) },
                new object[]{ "-1inch", new Value(-1, Unit.Inches) },
                new object[]{ "-1 inch", new Value(-1, Unit.Inches) },
                new object[]{ "10centimeters", new Value(10, Unit.Centimeters) },
                new object[]{ "10 centimeters", new Value(10, Unit.Centimeters) },
                new object[]{ "-10centimeters", new Value(-10, Unit.Centimeters) },
                new object[]{ "-10 centimeters", new Value(-10, Unit.Centimeters) },
                new object[]{ "10points", new Value(10, Unit.Points) },
                new object[]{ "10 points", new Value(10, Unit.Points) },
                new object[]{ "-10points", new Value(-10, Unit.Points) },
                new object[]{ "-10 points", new Value(-10, Unit.Points) },

                new object[]{ "10dpi(300)", new Value(10, Unit.Pixels(300)) },
                new object[]{ "10 dpi(300)", new Value(10, Unit.Pixels(300)) },
                new object[]{ "-10dpi(300)", new Value(-10, Unit.Pixels(300)) },
                new object[]{ "-10 dpi(300)", new Value(-10, Unit.Pixels(300)) },
            };
    }
}
