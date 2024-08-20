using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Synercoding.Primitives.JsonConverters;

public class RectangleJsonConverter : JsonConverter<Rectangle>
{
    public static RectangleJsonConverter Instance { get; } = new();

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

    public override void Write(Utf8JsonWriter writer, Rectangle value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
