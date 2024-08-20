using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Synercoding.Primitives.JsonConverters;

public class SizeJsonConverter : JsonConverter<Size>
{
    public static SizeJsonConverter Instance { get; } = new();

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

    public override void Write(Utf8JsonWriter writer, Size value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
