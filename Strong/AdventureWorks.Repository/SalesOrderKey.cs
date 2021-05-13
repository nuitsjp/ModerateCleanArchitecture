namespace AdventureWorks.Repository
{
    public class SalesOrderKey : ISalesOrderKey
    {
        internal static readonly SalesOrderKey InvalidValue = new (-1);
        
        public SalesOrderKey(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public override string ToString() => Value.ToString();
    }
}