using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdventureWorks.Presentation
{
    public class ConsoleView
    {
        private readonly ISalesOrderDetailRepository _salesOrderDetailRepository;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ConsoleView(ISalesOrderDetailRepository salesOrderDetailRepository)
        {
            _salesOrderDetailRepository = salesOrderDetailRepository;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                Converters =
                {
                    new SalesOrderDetailKeyConverter()
                },
                WriteIndented = true
            };
        }

        public async Task RunAsync()
        {
            Entry:
            Console.Write("対象のキーを入力してください。例）43659-10：");
            var input = await Console.In.ReadLineAsync()!;

            if (SalesOrderDetailKey.TryParse(input, out var key))
            {
                await WriteSalesOrderDetail(key);
            }
            else
            {
                await Console.Out.WriteLineAsync("キーを読み取れませんでした。");
                goto Entry;
            }
        }

        private async Task WriteSalesOrderDetail(SalesOrderDetailKey key)
        {
            var salesOrderDetail = await _salesOrderDetailRepository.GetSalesOrderDetailAsync(key);

            var salesOrderDetailString = JsonSerializer.Serialize(salesOrderDetail, _jsonSerializerOptions);
            Console.WriteLine(salesOrderDetailString);
        }
    }
}
