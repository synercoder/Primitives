namespace Synercoding.Primitives.Abstract
{
    /// <summary>
    /// Enable converting types between different units
    /// </summary>
    /// <typeparam name="T">The convertable type</typeparam>
    /// <remarks>The <typeparamref name="T"/> should be the implementing type.</remarks>
    public interface IConvertable<T>
    {
        /// <summary>
        /// Convert this type to the same type but with a different unit.
        /// </summary>
        /// <param name="unit">The target type.</param>
        /// <returns>A new value with different unit.</returns>
        T ConvertTo(Unit unit);
    }
}
