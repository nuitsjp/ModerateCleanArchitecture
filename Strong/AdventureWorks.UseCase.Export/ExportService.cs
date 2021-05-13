using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdventureWorks.UseCase.Export
{
    public class ExportService : IExportService
    {
        private readonly ISalesOrderDetailRepository _salesOrderDetailRepository;

        public ExportService(ISalesOrderDetailRepository salesOrderDetailRepository)
        {
            _salesOrderDetailRepository = salesOrderDetailRepository;
        }

        public async Task ExportAsync(ISalesOrderDetailKey salesOrderDetailKey, TextWriter textWriter)
        {
            var salesOrderDetail = await _salesOrderDetailRepository.GetSalesOrderDetailAsync(salesOrderDetailKey);

            var salesOrderDetailString =
                JsonSerializer.Serialize(
                    salesOrderDetail,
                    new JsonSerializerOptions
                    {
                        Converters =
                        {
                            KeyJsonConverterProvider.Provide<ISalesOrderDetailKey>()
                        },
                        WriteIndented = true
                    });
            await textWriter.WriteLineAsync(salesOrderDetailString);
        }
    }
}