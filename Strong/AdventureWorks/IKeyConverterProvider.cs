namespace AdventureWorks
{
    public interface IKeyConverterProvider
    {
        IKeyConverter<TKey> Provide<TKey>();
    }
}