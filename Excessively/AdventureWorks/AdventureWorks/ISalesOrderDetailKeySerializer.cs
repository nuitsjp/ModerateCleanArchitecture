namespace AdventureWorks
{
    public interface ISalesOrderDetailKeySerializer
    {
        bool TryDeserialize(string value, out ISalesOrderDetailKey key);

        string Serialize(ISalesOrderDetailKey key);
    }
}