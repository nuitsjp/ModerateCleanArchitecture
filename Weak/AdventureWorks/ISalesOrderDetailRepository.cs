using System.Threading.Tasks;

namespace AdventureWorks
{
    public interface ISalesOrderDetailRepository
    {
        Task<SalesOrderDetail?> GetSalesOrderDetailAsync(SalesOrderDetailKey key);
    }
}