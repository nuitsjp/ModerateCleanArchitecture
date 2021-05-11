using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdventureWorks.Presentation
{
    public class KeyConverter<TKey> : JsonConverter<TKey>
    {
        private readonly IKeySerializerProvider _keySerializerProvider;

        internal KeyConverter(IKeySerializerProvider keySerializerProvider)
        {
            _keySerializerProvider = keySerializerProvider;
        }

        public override TKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, TKey value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(_keySerializerProvider.Provide<TKey>().Serialize(value));
        }
    }
}