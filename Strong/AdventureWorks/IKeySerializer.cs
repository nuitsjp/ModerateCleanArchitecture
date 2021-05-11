namespace AdventureWorks
{
    public interface IKeySerializer<TKey>
    {
        bool TryDeserialize(string value, out TKey key);

        string Serialize(TKey key);

    }
}