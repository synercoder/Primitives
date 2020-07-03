using Xunit;

namespace Synercoding.Primitives.Tests
{
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
    }
}
