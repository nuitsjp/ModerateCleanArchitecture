namespace AdventureWorks
{
    public readonly struct SalesOrderKey
    {
        private readonly int _value;

        public SalesOrderKey(int value)
        {
            _value = value;
        }

        public int AsPrimitive() => _value;

        public override string ToString() => _value.ToString();
    }
}