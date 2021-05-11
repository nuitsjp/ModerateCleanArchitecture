namespace AdventureWorks.Repository
{
    public class KeySerializerProvider : IKeySerializerProvider
    {
        public KeySerializerProvider()
        {
            Cache<ISalesOrderDetailKey>.Value = new SalesOrderDetailKeySerializer();
        }

        public IKeySerializer<TKey> Provide<TKey>()
        {
            return Cache<TKey>.Value;
        }

        private static class Cache<T>
        {
            public static IKeySerializer<T> Value { get; set; } = null!;
        }
    }
}