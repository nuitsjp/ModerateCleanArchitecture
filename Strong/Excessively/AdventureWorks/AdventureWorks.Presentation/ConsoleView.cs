using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdventureWorks.Presentation
{
    public class ConsoleView
    {
        private readonly IKeySerializer<ISalesOrderDetailKey> _salesOrderDetailKeySerializer;
        private readonly ISalesOrderDetailRepository _salesOrderDetailRepository;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ConsoleView(
            IKeySerializerProvider keySerializerProvider, 
            IKeyConverterProvider keyConverterProvider,
            ISalesOrderDetailRepository salesOrderDetailRepository)
        {
            _salesOrderDetailKeySerializer = keySerializerProvider.Provide<ISalesOrderDetailKey>();
            _salesOrderDetailRepository = salesOrderDetailRepository;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                Converters =
                {
                    keyConverterProvider.Provide<ISalesOrderDetailKey>()
                },
                WriteIndented = true
            };
        }

        public async Task RunAsync()
        {
            Entry:
            Console.Write("対象のキーを入力してください。例）43659-10：");
            var input = await Console.In.ReadLineAsync()!;

            if (_salesOrderDetailKeySerializer.TryDeserialize(input, out var key))
            {
                await WriteSalesOrderDetail(key);
            }
            else
            {
                await Console.Out.WriteLineAsync("キーを読み取れませんでした。");
                goto Entry;
            }
        }

        private async Task WriteSalesOrderDetail(ISalesOrderDetailKey key)
        {
            var salesOrderDetail = await _salesOrderDetailRepository.GetSalesOrderDetailAsync(key);

            var salesOrderDetailString = JsonSerializer.Serialize(salesOrderDetail, _jsonSerializerOptions);
            Console.WriteLine(salesOrderDetailString);
        }
    }
}
