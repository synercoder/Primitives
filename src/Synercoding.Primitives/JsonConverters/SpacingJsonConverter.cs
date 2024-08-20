using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Synercoding.Primitives.JsonConverters;

public class SpacingJsonConverter : JsonConverter<Spacing>
{
    public static SpacingJsonConverter Instance { get; } = new();

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

    public override void Write(Utf8JsonWriter writer, Spacing value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
