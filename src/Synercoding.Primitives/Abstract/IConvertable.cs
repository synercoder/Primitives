namespace Synercoding.Primitives.Abstract;

/// <summary>
/// Enable converting types between different <see cref="Unit"/>s.
/// </summary>
/// <typeparam name="T">The convertable type.</typeparam>
/// <remarks>The <typeparamref name="T"/> should be the implementing type.</remarks>
public interface IConvertable<T>
{
    /// <summary>
    /// Convert this type to the same type but with a different <see cref="Unit"/>.
    /// </summary>
    /// <param name="unit">The target <see cref="Unit"/>.</param>
    /// <returns>A new <typeparamref name="T"/> with different <see cref="Unit"/>.</returns>
    T ConvertTo(Unit unit);
}
