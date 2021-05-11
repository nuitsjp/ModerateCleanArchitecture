using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdventureWorks.Presentation
{
    public class KeyJsonConverter<TKey> : JsonConverter<TKey>
    {
        public override TKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, TKey value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(KeyConverterProvider.Provide<TKey>().Convert(value));
        }
    }
}