using Synercoding.Primitives.Extensions;
using System;
using Xunit;

namespace Synercoding.Primitives.Tests;

public class UnitDesignationTests
{
    [Fact]
    public void Shortform_AllEnums_HaveShortform()
    {
        // Arrange        
        var unitDesignations = Enum.GetValues<UnitDesignation>();

        // Act
        foreach (var designation in unitDesignations)
            _ = designation.Shortform();

        // Assert
        // No NotImplementedException thrown! Jeej
    }

    [Fact]
    public void DefaultValueIsNotDefined()
    {
        // Arrange
        UnitDesignation value = 0;

        // Act
        var result = Enum.IsDefined(value);

        // Assert
        Assert.False(result);
    }
}
