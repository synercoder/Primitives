using Xunit;

namespace Synercoding.Primitives.Tests
{
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
    }
}
