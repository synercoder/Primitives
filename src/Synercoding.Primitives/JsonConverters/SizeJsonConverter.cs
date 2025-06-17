using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Synercoding.Primitives.JsonConverters;

/// <summary>
/// Converts a <see cref="Size"/> object to or from JSON.
/// </summary>
public class SizeJsonConverter : JsonConverter<Size>
{
    /// <summary>
    /// Thread-safe instance of <see cref="SizeJsonConverter"/> to be reused.
    /// </summary>
    public static SizeJsonConverter Instance { get; } = new();

    /// <summary>
    /// Read a <see cref="Size"/> object from the <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">The reader that should contain a <see cref="Value"/>.</param>
    /// <param name="typeToConvert">The expected type to convert.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    /// <returns>The <see cref="Size"/> that was read from the <paramref name="reader"/>.</returns>
    /// <exception cref="JsonException">Throws when the <paramref name="reader"/> does not contain <see cref="Size"/>.</exception>
    public override Size Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var textValue = reader.GetString() ?? throw new JsonException();

            if (Size.TryParse(textValue, out var value))
                return value;

            throw new JsonException();
        }

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        var complexSize = default(Size);
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return complexSize;

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propertyName = reader.GetString() ?? throw new JsonException();
                reader.Read();
                switch (propertyName)
                {
                    case nameof(Size.Width):
                        Value width = ValueJsonConverter.Instance.Read(ref reader, typeof(Value), options);
                        complexSize = complexSize with { Width = width };
                        break;
                    case nameof(Size.Height):
                        Value height = ValueJsonConverter.Instance.Read(ref reader, typeof(Value), options);
                        complexSize = complexSize with { Height = height };
                        break;
                }
            }
        }

        throw new JsonException();
    }

    /// <summary>
    /// Write a <see cref="Size"/> to the <paramref name="writer"/>.
    /// </summary>
    /// <param name="writer">Writer to write the <see cref="Size"/> to.</param>
    /// <param name="value">The <see cref="Size"/> to write.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    public override void Write(Utf8JsonWriter writer, Size value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
