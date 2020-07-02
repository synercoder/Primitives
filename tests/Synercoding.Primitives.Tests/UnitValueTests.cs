using System;
using System.Linq;
using Xunit;

namespace Synercoding.Primitives.Tests
{
    public class UnitValueTests
    {
        [Fact]
        public void LessThanOperator_MillimetersAndInch_IsSmaller()
        {
            // Arrange
            var mm = new UnitValue(1, Unit.Millimeters);
            var inch = new UnitValue(1, Unit.Inches);

            // Act
            var result = mm < inch;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOperator_PointsAndMillimeters_IsSmaller()
        {
            // Arrange
            var mm = new UnitValue(1, Unit.Millimeters);
            var point = new UnitValue(1, Unit.Points);

            // Act
            var result = point < mm;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualsOperator_25dot4MmAndOneInch_AreEqual()
        {
            // Arrange
            var mm = new UnitValue(25.4, Unit.Millimeters);
            var inch = new UnitValue(1, Unit.Inches);

            // Act
            var result = mm == inch;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Convert_OneInchToMillimeters_25dot4Millimeters()
        {
            // Arrange
            var inch = new UnitValue(1, Unit.Inches);

            // Act
            var mm = inch.ConvertTo(Unit.Millimeters);

            // Assert
            Assert.True(mm.Unit == Unit.Millimeters);
            Assert.True(mm.Value == 25.4);
        }

        [Fact]
        public void EqualsOperator_72pointsAndOneInch_AreEqual()
        {
            // Arrange
            var points = new UnitValue(72, Unit.Points);
            var inch = new UnitValue(1, Unit.Inches);

            // Act
            var result = points == inch;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualsOperator_600pxAt300dpiAndTwoInches_AreEqual()
        {
            // Arrange
            var pixels = new UnitValue(600, Unit.Pixels(300));
            var inch = new UnitValue(2, Unit.Inches);

            // Act
            var result = pixels == inch;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AdditionOperator_MillimetersAndInches_ResultIs26dot4mm()
        {
            // Arrange
            var mm = new UnitValue(1, Unit.Millimeters);
            var inch = new UnitValue(1, Unit.Inches);
            var expected = new UnitValue(26.4, Unit.Millimeters);

            // Act
            var result = mm + inch;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DivisionOperator_1cmAnd1mm_containes10times()
        {
            // Arrange
            var cm = new UnitValue(1, Unit.Centimeters);
            var mm = new UnitValue(1, Unit.Millimeters);
            var expected = 10;

            // Act
            var result = cm / mm;

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
    }
}
