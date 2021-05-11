namespace AdventureWorks
{
    public static class KeyConverterProvider
    {
        public static void Register<TKey>(IKeyConverter<TKey> converter)
        {
            Cache<TKey>.Value = converter;
        }

        public static IKeyConverter<TKey> Provide<TKey>()
        {
            return Cache<TKey>.Value;
        }

        private static class Cache<T>
        {
            public static IKeyConverter<T> Value { get; set; } = null!;
        }

    }
}