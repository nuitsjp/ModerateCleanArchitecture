namespace AdventureWorks.Repository
{
    public class KeyConverterProvider : IKeyConverterProvider
    {
        public KeyConverterProvider()
        {
            Cache<ISalesOrderDetailKey>.Value = new SalesOrderDetailKeyConverter();
        }

        public IKeyConverter<TKey> Provide<TKey>()
        {
            return Cache<TKey>.Value;
        }

        private static class Cache<T>
        {
            public static IKeyConverter<T> Value { get; set; } = null!;
        }
    }
}