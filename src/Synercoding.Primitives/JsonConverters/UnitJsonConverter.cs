using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Synercoding.Primitives.JsonConverters;

public class UnitJsonConverter : JsonConverter<Unit>
{
    public static UnitJsonConverter Instance { get; } = new();

    public override Unit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException();

        var textValue = reader.GetString() ?? throw new JsonException();

        if (Unit.TryParse(textValue, out var unit))
            return unit;

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, Unit value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

