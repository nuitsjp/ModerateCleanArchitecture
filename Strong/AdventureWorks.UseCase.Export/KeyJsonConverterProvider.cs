using System.Text.Json.Serialization;

namespace AdventureWorks.UseCase.Export
{
    internal static class KeyJsonConverterProvider
    {
        static KeyJsonConverterProvider()
        {
            Cache<ISalesOrderDetailKey>.Value = new KeyJsonConverter<ISalesOrderDetailKey>();
        }

        internal static JsonConverter<TKey> Provide<TKey>()
        {
            return Cache<TKey>.Value;
        }

        private static class Cache<T>
        {
            public static JsonConverter<T> Value { get; set; } = null!;
        }

    }
}