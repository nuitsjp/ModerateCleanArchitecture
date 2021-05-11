using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdventureWorks.Presentation
{
    public class ConsoleView
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

        private async Task<SalesOrderDetailKey> ReadSalesOrderDetailKeyAsync()
        {
            do
            {
                Console.Write("対象のキーを入力してください。例）43659-10：");
                var input = await Console.In.ReadLineAsync()!;

                if (SalesOrderDetailKey.TryParse(input, out var key))
                {
                    return key;
                }

                await Console.Out.WriteLineAsync("キーを読み取れませんでした。");
            } while (true);
        }

        private async Task WriteSalesOrderDetail(SalesOrderDetailKey key)
        {
            var salesOrderDetail = await _salesOrderDetailRepository.GetSalesOrderDetailAsync(key);

            var salesOrderDetailString =
                JsonSerializer.Serialize(
                    salesOrderDetail,
                    new JsonSerializerOptions
                    {
                        Converters =
                        {
                            new SalesOrderDetailKeyConverter()
                        },
                        WriteIndented = true
                    });
            Console.WriteLine(salesOrderDetailString);
        }
    }
}
