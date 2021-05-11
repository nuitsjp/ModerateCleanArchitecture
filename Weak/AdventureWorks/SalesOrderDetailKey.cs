namespace AdventureWorks
{
    public readonly struct SalesOrderDetailKey
    {
        private readonly SalesOrderKey _salesOrderKey;
        private readonly int _salesOrderDetailId;

        public SalesOrderDetailKey(SalesOrderKey salesOrderKey, int salesOrderDetailId)
        {
            _salesOrderKey = salesOrderKey;
            _salesOrderDetailId = salesOrderDetailId;
        }

        public SalesOrderKey GetSalesOrderKey() => _salesOrderKey;
        public int GetSalesOrderDetailId() => _salesOrderDetailId;

        public static bool TryParse(string value, out SalesOrderDetailKey key)
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
                key = default;
                return false;
            }
        }

        public override string ToString() => $"{_salesOrderKey}-{_salesOrderDetailId}";
    }
}