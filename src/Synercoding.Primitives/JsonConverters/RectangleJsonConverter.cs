using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Synercoding.Primitives.JsonConverters;

/// <summary>
/// Converts a <see cref="Rectangle"/> object to or from JSON.
/// </summary>
public class RectangleJsonConverter : JsonConverter<Rectangle>
{
    /// <summary>
    /// Thread-safe instance of <see cref="RectangleJsonConverter"/> to be reused.
    /// </summary>
    public static RectangleJsonConverter Instance { get; } = new();

    /// <summary>
    /// Read a <see cref="Rectangle"/> object from the <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">The reader that should contain a <see cref="Rectangle"/>.</param>
    /// <param name="typeToConvert">The expected type to convert.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    /// <returns>The <see cref="Rectangle"/> that was read from the <paramref name="reader"/>.</returns>
    /// <exception cref="JsonException">Throws when the <paramref name="reader"/> does not contain <see cref="Rectangle"/>.</exception>
    public override Rectangle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var textValue = reader.GetString() ?? throw new JsonException();

            if (Rectangle.TryParse(textValue, out var value))
                return value;

            throw new JsonException();
        }

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        var complexRectangle = default(Rectangle);
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return complexRectangle;

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propertyName = reader.GetString() ?? throw new JsonException();
                reader.Read();
                switch (propertyName)
                {
                    case nameof(Rectangle.LLX):
                        Value llx = ValueJsonConverter.Instance.Read(ref reader, typeof(Value), options);
                        complexRectangle = complexRectangle with { LLX = llx };
                        break;
                    case nameof(Rectangle.LLY):
                        Value lly = ValueJsonConverter.Instance.Read(ref reader, typeof(Value), options);
                        complexRectangle = complexRectangle with { LLY = lly };
                        break;
                    case nameof(Rectangle.URX):
                        Value urx = ValueJsonConverter.Instance.Read(ref reader, typeof(Value), options);
                        complexRectangle = complexRectangle with { URX = urx };
                        break;
                    case nameof(Rectangle.URY):
                        Value ury = ValueJsonConverter.Instance.Read(ref reader, typeof(Value), options);
                        complexRectangle = complexRectangle with { URY = ury };
                        break;
                }
            }
        }

        throw new JsonException();
    }

    /// <summary>
    /// Write a <see cref="Rectangle"/> to the <paramref name="writer"/>.
    /// </summary>
    /// <param name="writer">Writer to write the <see cref="Rectangle"/> to.</param>
    /// <param name="value">The <see cref="Rectangle"/> to write.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    public override void Write(Utf8JsonWriter writer, Rectangle value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
