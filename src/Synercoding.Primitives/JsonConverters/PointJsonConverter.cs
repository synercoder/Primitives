using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Synercoding.Primitives.JsonConverters;

/// <summary>
/// Converts a <see cref="Point"/> object to or from JSON.
/// </summary>
public class PointJsonConverter : JsonConverter<Point>
{
    /// <summary>
    /// Thread-safe instance of <see cref="PointJsonConverter"/> to be reused.
    /// </summary>
    public static PointJsonConverter Instance { get; } = new();

    /// <summary>
    /// Read a <see cref="Point"/> object from the <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">The reader that should contain a <see cref="Point"/>.</param>
    /// <param name="typeToConvert">The expected type to convert.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    /// <returns>The <see cref="Point"/> that was read from the <paramref name="reader"/>.</returns>
    /// <exception cref="JsonException">Throws when the <paramref name="reader"/> does not contain <see cref="Point"/>.</exception>
    public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var textValue = reader.GetString() ?? throw new JsonException();

            if (Point.TryParse(textValue, out var value))
                return value;

            throw new JsonException();
        }

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        var complexPoint = default(Point);
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return complexPoint;

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propertyName = reader.GetString() ?? throw new JsonException();
                reader.Read();
                switch (propertyName)
                {
                    case nameof(Point.X):
                        Value width = ValueJsonConverter.Instance.Read(ref reader, typeof(Value), options);
                        complexPoint = complexPoint with { X = width };
                        break;
                    case nameof(Point.Y):
                        Value height = ValueJsonConverter.Instance.Read(ref reader, typeof(Value), options);
                        complexPoint = complexPoint with { Y = height };
                        break;
                }
            }
        }

        throw new JsonException();
    }

    /// <summary>
    /// Write a <see cref="Point"/> to the <paramref name="writer"/>.
    /// </summary>
    /// <param name="writer">Writer to write the <see cref="Point"/> to.</param>
    /// <param name="value">The <see cref="Point"/> to write.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}