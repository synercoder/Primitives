using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Synercoding.Primitives.JsonConverters;

/// <summary>
/// Converts a <see cref="Value"/> object to or from JSON.
/// </summary>
public class ValueJsonConverter : JsonConverter<Value>
{
    /// <summary>
    /// Thread-safe instance of <see cref="ValueJsonConverter"/> to be reused.
    /// </summary>
    public static ValueJsonConverter Instance { get; } = new();

    /// <summary>
    /// Read a <see cref="Value"/> object from the <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">The reader that should contain a <see cref="Value"/>.</param>
    /// <param name="typeToConvert">The expected type to convert.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    /// <returns>The <see cref="Value"/> that was read from the <paramref name="reader"/>.</returns>
    /// <exception cref="JsonException">Throws when the <paramref name="reader"/> does not contain <see cref="Value"/>.</exception>
    public override Value Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var textValue = reader.GetString() ?? throw new JsonException();

            if (Value.TryParse(textValue, CultureInfo.InvariantCulture, out var value))
                return value;

            throw new JsonException();
        }

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        var complexValue = default(Value);
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return complexValue;

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propertyName = reader.GetString() ?? throw new JsonException();
                reader.Read();
                switch (propertyName)
                {
                    case nameof(Value.Raw):
                        double raw = reader.GetDouble();
                        complexValue = complexValue with { Raw = raw };
                        break;
                    case nameof(Value.Unit):
                        Unit unit = UnitJsonConverter.Instance.Read(ref reader, typeof(Unit), options);
                        complexValue = complexValue with { Unit = unit };
                        break;
                }
            }
        }

        throw new JsonException();
    }

    /// <summary>
    /// Write a <see cref="Value"/> to the <paramref name="writer"/>.
    /// </summary>
    /// <param name="writer">Writer to write the <see cref="Value"/> to.</param>
    /// <param name="value">The <see cref="Value"/> to write.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    public override void Write(Utf8JsonWriter writer, Value value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
    }
}
