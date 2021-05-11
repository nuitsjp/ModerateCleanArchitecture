using System.Threading.Tasks;
using Dapper;

namespace AdventureWorks.Repository
{
    public class SalesOrderDetailRepository : ISalesOrderDetailRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public SalesOrderDetailRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;

        }

        public async Task<SalesOrderDetail?> GetSalesOrderDetailAsync(ISalesOrderDetailKey key)
        {
            using var connection = _dbConnectionFactory.Open();

            SqlMapper.AddTypeHandler(new SalesOrderKeyTypeHandler());

            var internalKey = (SalesOrderDetailKey) key;

            var dynamic = await connection.QuerySingleAsync(@"
select 
	[SalesOrderID],
	[SalesOrderDetailID],
	[CarrierTrackingNumber],
	[OrderQty],
	[ProductID],
	[SpecialOfferID],
	[UnitPrice],
	[UnitPriceDiscount],
	[LineTotal],
	[ModifiedDate]
from 
	[AdventureWorks].[Sales].[SalesOrderDetail]
where
    [SalesOrderID] = @SalesOrderId
    and [SalesOrderDetailID] = @SalesOrderDetailId
",
                new
                {
                    SalesOrderId = internalKey.SalesOrderKey,
                    internalKey.SalesOrderDetailId
                });

            if (dynamic is null) return null;

            return new SalesOrderDetail(
                new SalesOrderDetailKey(
                    new SalesOrderKey(dynamic.SalesOrderID),
                    dynamic.SalesOrderDetailID),
                dynamic.CarrierTrackingNumber,
                dynamic.OrderQty,
                dynamic.ProductID,
                dynamic.SpecialOfferID,
                dynamic.UnitPrice,
                dynamic.UnitPriceDiscount,
                dynamic.LineTotal,
                dynamic.ModifiedDate
            );
        }
    }
}
