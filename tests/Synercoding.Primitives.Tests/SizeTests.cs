using Xunit;

namespace Synercoding.Primitives.Tests
{
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
    }
}
