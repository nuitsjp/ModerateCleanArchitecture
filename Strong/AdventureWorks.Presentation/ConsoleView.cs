using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Presentation
{
    public class ConsoleView : BackgroundService 
    {
        private readonly ISalesOrderDetailRepository _salesOrderDetailRepository;

        private readonly ILogger<ConsoleView> _logger;

        public ConsoleView(ISalesOrderDetailRepository salesOrderDetailRepository, ILogger<ConsoleView> logger)
        {
            _salesOrderDetailRepository = salesOrderDetailRepository;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var salesOrderDetailKey = await ReadSalesOrderDetailKeyAsync();
            _logger.LogInformation("salesOrderDetailKey:{0}", salesOrderDetailKey);
            await WriteSalesOrderDetail(salesOrderDetailKey);
        }

        private async Task<ISalesOrderDetailKey> ReadSalesOrderDetailKeyAsync()
        {
            do
            {
                Console.Write("対象のキーを入力してください。例）43659-10：");
                var input = await Console.In.ReadLineAsync()!;

                if (KeyConverterProvider.Provide<ISalesOrderDetailKey>().TryConvert(input, out var key))
                {
                    return key;
                }

                await Console.Out.WriteLineAsync("キーを読み取れませんでした。");
            } while (true);
        }

        private async Task WriteSalesOrderDetail(ISalesOrderDetailKey key)
        {
            var salesOrderDetail = await _salesOrderDetailRepository.GetSalesOrderDetailAsync(key);

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
            Console.WriteLine(salesOrderDetailString);
        }
    }
}
