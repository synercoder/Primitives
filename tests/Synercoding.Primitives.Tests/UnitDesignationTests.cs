using Synercoding.Primitives.Extensions;
using System;
using System.Linq;
using Xunit;

namespace Synercoding.Primitives.Tests
{
    public class UnitDesignationTests
    {
        [Fact]
        public void Shortform_AllEnums_HaveShortform()
        {
            // Arrange
            var unitDesignations = Enum.GetValues(typeof(UnitDesignation)).OfType<UnitDesignation>();

            // Act
            foreach (var designation in unitDesignations)
                _ = designation.Shortform();

            // Assert
            // No NotImplementedException thrown! Jeej
        }
    }
}
