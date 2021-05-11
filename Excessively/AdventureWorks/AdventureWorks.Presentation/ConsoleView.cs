using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdventureWorks.Presentation
{
    public class ConsoleView
    {
        private readonly ISalesOrderDetailKeySerializer _salesOrderDetailKeySerializer;
        private readonly ISalesOrderDetailRepository _salesOrderDetailRepository;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ConsoleView(
            ISalesOrderDetailKeySerializer salesOrderDetailKeySerializer,
            ISalesOrderDetailRepository salesOrderDetailRepository, 
            SalesOrderDetailKeyConverter salesOrderDetailKeyConverter)
        {
            _salesOrderDetailKeySerializer = salesOrderDetailKeySerializer;
            _salesOrderDetailRepository = salesOrderDetailRepository;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                Converters =
                {
                    salesOrderDetailKeyConverter
                },
                WriteIndented = true
            };
        }

        public async Task RunAsync()
        {
            Entry:
            Console.Write("対象のキーを入力してください。例）43659-10：");
            var input = Console.ReadLine()!;

            if (_salesOrderDetailKeySerializer.TryDeserialize(input, out var key))
            {
                await WriteSalesOrderDetail(key);
            }
            else
            {
                Console.WriteLine("キーを読み取れませんでした。");
                goto Entry;
            }
        }

        private async Task WriteSalesOrderDetail(ISalesOrderDetailKey? key)
        {
            var salesOrderDetail = await _salesOrderDetailRepository.GetSalesOrderDetailAsync(key);

            var salesOrderDetailString = JsonSerializer.Serialize(salesOrderDetail, _jsonSerializerOptions);
            Console.WriteLine(salesOrderDetailString);
        }
    }
}
