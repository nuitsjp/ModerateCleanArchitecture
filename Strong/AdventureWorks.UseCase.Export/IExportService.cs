using System.IO;
using System.Threading.Tasks;

namespace AdventureWorks.UseCase.Export
{
    public interface IExportService
    {
        Task ExportAsync(ISalesOrderDetailKey salesOrderDetailKey, TextWriter textWriter);
    }
}