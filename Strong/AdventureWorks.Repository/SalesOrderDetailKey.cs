namespace AdventureWorks.Repository
{
    public class SalesOrderDetailKey : ISalesOrderDetailKey
    {
        public SalesOrderDetailKey(SalesOrderKey salesOrderKey, int salesOrderDetailId)
        {
            SalesOrderKey = salesOrderKey;
            SalesOrderDetailId = salesOrderDetailId;
        }

        internal SalesOrderKey SalesOrderKey { get; }

        internal int SalesOrderDetailId { get; }
    }
}