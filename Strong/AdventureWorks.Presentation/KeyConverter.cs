using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdventureWorks.Presentation
{
    public class KeyConverter<TKey> : JsonConverter<TKey>
    {
        private readonly AdventureWorks.IKeyConverterProvider _keyConverterProvider;

        internal KeyConverter(AdventureWorks.IKeyConverterProvider keyConverterProvider)
        {
            _keyConverterProvider = keyConverterProvider;
        }

        public override TKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, TKey value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(_keyConverterProvider.Provide<TKey>().Convert(value));
        }
    }
}