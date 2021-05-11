using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdventureWorks.Presentation
{
    public class ConsoleView
    {
        private readonly IKeyConverter<ISalesOrderDetailKey> _salesOrderDetailKeyConverter;
        private readonly ISalesOrderDetailRepository _salesOrderDetailRepository;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ConsoleView(
            IKeyConverterProvider keyConverterProvider, 
            IKeyJsonConverterProvider keyJsonConverterProvider,
            ISalesOrderDetailRepository salesOrderDetailRepository)
        {
            _salesOrderDetailKeyConverter = keyConverterProvider.Provide<ISalesOrderDetailKey>();
            _salesOrderDetailRepository = salesOrderDetailRepository;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                Converters =
                {
                    keyJsonConverterProvider.Provide<ISalesOrderDetailKey>()
                },
                WriteIndented = true
            };
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

                if (_salesOrderDetailKeyConverter.TryConvert(input, out var key))
                {
                    return key;
                }

                await Console.Out.WriteLineAsync("キーを読み取れませんでした。");
            } while (true);
        }

        private async Task WriteSalesOrderDetail(ISalesOrderDetailKey key)
        {
            var salesOrderDetail = await _salesOrderDetailRepository.GetSalesOrderDetailAsync(key);

            var salesOrderDetailString = JsonSerializer.Serialize(salesOrderDetail, _jsonSerializerOptions);
            Console.WriteLine(salesOrderDetailString);
        }
    }
}
