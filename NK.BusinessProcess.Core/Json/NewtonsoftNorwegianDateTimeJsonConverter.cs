using System;
using Newtonsoft.Json;

namespace NK.BusinessProcess.Core.Json;

public class NewtonsoftNorwegianDateTimeJsonConverter : JsonConverter<DateTime>
{
    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        writer.WriteValue(value);
    }

    public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        DateTime.TryParse(reader.Value.ToString(), out var date);
        return date;
    }
}

public class NewtonsoftNorwegianNullableDateTimeJsonConverter : JsonConverter<DateTime?>
{
    public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
    {
        writer.WriteValue(value.Value);
    }

    public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        DateTime.TryParse(reader.Value.ToString(), out var date);
        return date;
    }
}
