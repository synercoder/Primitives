using System;

namespace Synercoding.Primitives.Extensions
{
    /// <summary>
    /// Extensions for <see cref="UnitDesignation"/>
    /// </summary>
    public static class UnitDesignationExtensions
    {
        /// <summary>
        /// Get the shortform notation for a <see cref="UnitDesignation"/>
        /// </summary>
        /// <param name="designation">The designation to get the shortform for</param>
        /// <returns>The shortform notation for a <see cref="UnitDesignation"/></returns>
        public static string Shortform(this UnitDesignation designation)
            => designation switch
            {
                UnitDesignation.Centimeters => "cm",
                UnitDesignation.Inches => "in",
                UnitDesignation.Millimeters => "mm",
                UnitDesignation.Pixels => "px",
                UnitDesignation.Points => "pts",
                UnitDesignation x => throw new NotImplementedException($"{x} has no shortform.")
            };
    }
}
