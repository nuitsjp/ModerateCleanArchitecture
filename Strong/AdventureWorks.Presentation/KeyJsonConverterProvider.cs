using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AdventureWorks.Presentation
{
    public static class KeyJsonConverterProvider
    {
        static KeyJsonConverterProvider()
        {
            Cache<ISalesOrderDetailKey>.Value = new KeyJsonConverter<ISalesOrderDetailKey>();
        }

        public static JsonConverter<TKey> Provide<TKey>()
        {
            return Cache<TKey>.Value;
        }

        private static class Cache<T>
        {
            public static JsonConverter<T> Value { get; set; } = null!;
        }

    }
}