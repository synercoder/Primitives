using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Synercoding.Primitives.JsonConverters;

/// <summary>
/// Converts a <see cref="Spacing"/> object to or from JSON.
/// </summary>
public class SpacingJsonConverter : JsonConverter<Spacing>
{
    /// <summary>
    /// Thread-safe instance of <see cref="SpacingJsonConverter"/> to be reused.
    /// </summary>
    public static SpacingJsonConverter Instance { get; } = new();

    /// <summary>
    /// Read a <see cref="Spacing"/> object from the <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">The reader that should contain a <see cref="Spacing"/>.</param>
    /// <param name="typeToConvert">The expected type to convert.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    /// <returns>The <see cref="Spacing"/> that was read from the <paramref name="reader"/>.</returns>
    /// <exception cref="JsonException">Throws when the <paramref name="reader"/> does not contain <see cref="Spacing"/>.</exception>
    public override Spacing Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var textValue = reader.GetString() ?? throw new JsonException();

            if (Spacing.TryParse(textValue, out var value))
                return value;

            throw new JsonException();
        }

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        var complexSpacing = default(Spacing);
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return complexSpacing;

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propertyName = reader.GetString() ?? throw new JsonException();
                reader.Read();
                switch (propertyName)
                {
                    case nameof(Spacing.Left):
                        Value left = ValueJsonConverter.Instance.Read(ref reader, typeof(Value), options);
                        complexSpacing = complexSpacing with { Left = left };
                        break;
                    case nameof(Spacing.Right):
                        Value right = ValueJsonConverter.Instance.Read(ref reader, typeof(Value), options);
                        complexSpacing = complexSpacing with { Right = right };
                        break;
                    case nameof(Spacing.Top):
                        Value top = ValueJsonConverter.Instance.Read(ref reader, typeof(Value), options);
                        complexSpacing = complexSpacing with { Top = top };
                        break;
                    case nameof(Spacing.Bottom):
                        Value bottom = ValueJsonConverter.Instance.Read(ref reader, typeof(Value), options);
                        complexSpacing = complexSpacing with { Bottom = bottom };
                        break;
                }
            }
        }

        throw new JsonException();
    }

    /// <summary>
    /// Write a <see cref="Spacing"/> to the <paramref name="writer"/>.
    /// </summary>
    /// <param name="writer">Writer to write the <see cref="Spacing"/> to.</param>
    /// <param name="value">The <see cref="Spacing"/> to write.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    public override void Write(Utf8JsonWriter writer, Spacing value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
    }
}
