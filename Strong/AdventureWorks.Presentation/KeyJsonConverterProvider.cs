using System.Text.Json.Serialization;

namespace AdventureWorks.Presentation
{
    public class KeyJsonConverterProvider : IKeyJsonConverterProvider
    {
        public KeyJsonConverterProvider(AdventureWorks.IKeyConverterProvider keyConverterProvider)
        {
            Cache<ISalesOrderDetailKey>.Value = new KeyJsonConverter<ISalesOrderDetailKey>(keyConverterProvider);
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