using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Synercoding.Primitives.JsonConverters;

public class PointJsonConverter : JsonConverter<Point>
{
    public static PointJsonConverter Instance { get; } = new();

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

    public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}