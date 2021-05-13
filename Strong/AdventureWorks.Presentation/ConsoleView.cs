using System;
using System.Threading;
using System.Threading.Tasks;
using AdventureWorks.UseCase.Export;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Presentation
{
    public class ConsoleView : BackgroundService
    {
        private readonly IExportService _exportService;

        private readonly ILogger<ConsoleView> _logger;

        public ConsoleView(IExportService exportService, ILogger<ConsoleView> logger)
        {
            _exportService = exportService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var salesOrderDetailKey = await ReadSalesOrderDetailKeyAsync();
            _logger.LogInformation("salesOrderDetailKey:{0}", salesOrderDetailKey);
            await WriteSalesOrderDetail(salesOrderDetailKey);
        }

        private static async Task<ISalesOrderDetailKey> ReadSalesOrderDetailKeyAsync()
        {
            do
            {
                await Console.Out.WriteAsync("対象のキーを入力してください。例）43659-10：");
                var input = await Console.In.ReadLineAsync()!;

                if (KeyConverterProvider.Provide<ISalesOrderDetailKey>().TryConvert(input, out var key))
                {
                    return key;
                }

                await Console.Out.WriteLineAsync("キーを読み取れませんでした。");
            } while (true);
        }

        private Task WriteSalesOrderDetail(ISalesOrderDetailKey key)
        {
            return _exportService.ExportAsync(key, Console.Out);
        }
    }
}
