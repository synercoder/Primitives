using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Synercoding.Primitives.JsonConverters;

public class ValueJsonConverter : JsonConverter<Value>
{
    public static ValueJsonConverter Instance { get; } = new();

    public override Value Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var textValue = reader.GetString() ?? throw new JsonException();

            if (Value.TryParse(textValue, out var value))
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

    public override void Write(Utf8JsonWriter writer, Value value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
