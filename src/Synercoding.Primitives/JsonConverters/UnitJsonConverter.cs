using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Synercoding.Primitives.JsonConverters;

/// <summary>
/// Converts a <see cref="Unit"/> object to or from JSON.
/// </summary>
public class UnitJsonConverter : JsonConverter<Unit>
{
    /// <summary>
    /// Thread-safe instance of <see cref="UnitJsonConverter"/> to be reused.
    /// </summary>
    public static UnitJsonConverter Instance { get; } = new();

    /// <summary>
    /// Read a <see cref="Unit"/> object from the <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">The reader that should contain a <see cref="Unit"/>.</param>
    /// <param name="typeToConvert">The expected type to convert.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    /// <returns>The <see cref="Unit"/> that was read from the <paramref name="reader"/>.</returns>
    /// <exception cref="JsonException">Throws when the <paramref name="reader"/> does not contain <see cref="Unit"/>.</exception>
    public override Unit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException();

        var textValue = reader.GetString() ?? throw new JsonException();

        if (Unit.TryParse(textValue, CultureInfo.InvariantCulture, out var unit))
            return unit;

        throw new JsonException();
    }

    /// <summary>
    /// Write a <see cref="Unit"/> to the <paramref name="writer"/>.
    /// </summary>
    /// <param name="writer">Writer to write the <see cref="Unit"/> to.</param>
    /// <param name="value">The <see cref="Unit"/> to write.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    public override void Write(Utf8JsonWriter writer, Unit value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

