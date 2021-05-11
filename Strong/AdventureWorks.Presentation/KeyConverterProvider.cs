using System.Text.Json.Serialization;

namespace AdventureWorks.Presentation
{
    public class KeyConverterProvider : IKeyConverterProvider
    {
        public KeyConverterProvider(AdventureWorks.IKeyConverterProvider keyConverterProvider)
        {
            Cache<ISalesOrderDetailKey>.Value = new KeyConverter<ISalesOrderDetailKey>(keyConverterProvider);
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