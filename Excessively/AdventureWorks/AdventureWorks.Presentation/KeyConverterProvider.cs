using System.Text.Json.Serialization;

namespace AdventureWorks.Presentation
{
    public class KeyConverterProvider : IKeyConverterProvider
    {
        public KeyConverterProvider(IKeySerializerProvider keySerializerProvider)
        {
            Cache<ISalesOrderDetailKey>.Value = new KeyConverter<ISalesOrderDetailKey>(keySerializerProvider);
        }

        public JsonConverter<TKey> Provide<TKey>()
        {
            return Cache<TKey>.Value;
        }

        private static class Cache<T>
        {
            public static JsonConverter<T> Value { get; set; } = null!;
        }

    }
}