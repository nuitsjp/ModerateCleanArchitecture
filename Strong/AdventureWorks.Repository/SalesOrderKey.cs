namespace AdventureWorks.Repository
{
    public partial class SalesOrderKey : ISalesOrderKey
    {
        internal static readonly SalesOrderKey InvalidValue = new (-1);
    }
}