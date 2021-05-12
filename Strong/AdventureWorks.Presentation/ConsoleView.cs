using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace AdventureWorks.Presentation
{
    public class ConsoleView : IHostedService
    {
        private readonly ISalesOrderDetailRepository _salesOrderDetailRepository;

        public ConsoleView(ISalesOrderDetailRepository salesOrderDetailRepository)
        {
            _salesOrderDetailRepository = salesOrderDetailRepository;
        }

        public async Task RunAsync()
        {
            var salesOrderDetailKey = await ReadSalesOrderDetailKeyAsync();
            await WriteSalesOrderDetail(salesOrderDetailKey);
        }

        public async Task<ISalesOrderDetailKey> ReadSalesOrderDetailKeyAsync()
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

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var salesOrderDetailKey = await ReadSalesOrderDetailKeyAsync();
            await WriteSalesOrderDetail(salesOrderDetailKey);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
