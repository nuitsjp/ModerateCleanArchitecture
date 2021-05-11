namespace AdventureWorks
{
    public interface IKeyConverter<TKey>
    {
        bool TryConvert(string value, out TKey key);

        string Convert(TKey key);
    }
}