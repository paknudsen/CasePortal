using System;
#if NET6_0_OR_GREATER
using System.Text.Json;
using System.Text.Json.Serialization;
#endif

#if NET472
using Newtonsoft.Json;
#endif

namespace NK.BusinessProcess.Core.Json
{
#if NET472
    /// <summary>
    /// .NET Core has decided that ISO 8601-1:2019 format should rule them all, 
    /// but thats not practical for us in the Nordics so we use the more lenient DateTime parser
    /// https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-converters-how-to#sample-basic-converter
    /// </summary>
    public class NorwegianDateTimeJsonConverter : JsonConverter<DateTime>
    {
        // We want to use the builtin converter for writing
        public override bool CanWrite => false;
        public override bool CanRead => true;

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            DateTime.TryParse(reader?.Value?.ToString(), out var date);
            return date;
        }
    }

    public class NorwegianNullableDateTimeJsonConverter : JsonConverter<DateTime?>
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;

        public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            DateTime.TryParse(reader?.Value?.ToString(), out var date);
            return date > DateTime.MinValue ? new DateTime?(date) : null;
        }
    }
#endif

#if NET6_0_OR_GREATER
    /// <summary>
    ///     .NET Core has decided that ISO 8601-1:2019 format should rule them all,
    ///     but thats not practical for us in the Nordics so we use the more lenient DateTime parser
    ///     https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-converters-how-to#sample-basic-converter
    /// </summary>
    public class NorwegianDateTimeJsonConverter : JsonConverter<DateTime>
    {
        // We want to use the builtin converter for writing
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateTime.TryParse(reader.GetString(), out var date);
            return date;
        }
    }

    public class NorwegianNullableDateTimeJsonConverter : JsonConverter<DateTime?>
    {
        // We want to use the builtin converter for writing
        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateTime.TryParse(reader.GetString(), out var date);
            return date;
        }
    }
#endif
}
