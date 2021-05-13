namespace AdventureWorks.Repository
{
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
                key = new SalesOrderDetailKey(new SalesOrderKey(0), 0);
                return false;
            }
        }

        public string Convert(ISalesOrderDetailKey key) => key.ToString();
    }
}