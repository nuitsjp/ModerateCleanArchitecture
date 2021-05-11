namespace AdventureWorks.Repository
{
    public class SalesOrderDetailKeySerializer : IKeySerializer<ISalesOrderDetailKey>
    {
        public bool TryDeserialize(string value, out ISalesOrderDetailKey key)
        {
            try
            {
                var columns = value.Split('-');
                key = new SalesOrderDetailKey(
                    new SalesOrderKey(int.Parse(columns[0])),
                    int.Parse(columns[1]));
                return true;
            }
            catch
            {
                key = new SalesOrderDetailKey(new SalesOrderKey(0), 0);
                return false;
            }
        }

        public string Serialize(ISalesOrderDetailKey key)
        {
            var internalKey = (SalesOrderDetailKey) key;
            return $"{internalKey.SalesOrderKey.Value}-{internalKey.SalesOrderDetailId}";
        }
    }
}