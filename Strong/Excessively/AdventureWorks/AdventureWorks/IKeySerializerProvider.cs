namespace AdventureWorks
{
    public interface IKeySerializerProvider
    {
        IKeySerializer<TKey> Provide<TKey>();
    }
}