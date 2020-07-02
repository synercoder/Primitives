using Synercoding.Primitives.Extensions;
using Xunit;

namespace Synercoding.Primitives.Tests
{
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
    }
}
