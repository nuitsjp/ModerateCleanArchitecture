namespace AdventureWorks.Repository
{
    public class SalesOrderDetailKey : ISalesOrderDetailKey
    {
        internal static readonly SalesOrderDetailKey InvalidValue = new (SalesOrderKey.InvalidValue, -1);

        public SalesOrderDetailKey(SalesOrderKey salesOrderKey, int salesOrderDetailId)
        {
            SalesOrderKey = salesOrderKey;
            SalesOrderDetailId = salesOrderDetailId;
        }

        internal SalesOrderKey SalesOrderKey { get; }

        internal int SalesOrderDetailId { get; }

        public override string ToString() => $"{SalesOrderKey}-{SalesOrderDetailId}";
        
        public class SalesOrderDetailKeyConverter : IKeyConverter<ISalesOrderDetailKey>
        {
            public bool TryConvert(string value, out ISalesOrderDetailKey key)
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
                    key = InvalidValue;
                    return false;
                }
            }

            public string Convert(ISalesOrderDetailKey key) => key.ToString();
        }

    }
}