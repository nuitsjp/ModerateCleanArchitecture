using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdventureWorks.Presentation
{
    public class SalesOrderDetailKeyConverter : JsonConverter<ISalesOrderDetailKey>
    {
        private readonly ISalesOrderDetailKeySerializer _salesOrderDetailKeySerializer;

        public SalesOrderDetailKeyConverter(ISalesOrderDetailKeySerializer salesOrderDetailKeySerializer)
        {
            _salesOrderDetailKeySerializer = salesOrderDetailKeySerializer;
        }

        public override ISalesOrderDetailKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, ISalesOrderDetailKey value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(_salesOrderDetailKeySerializer.Serialize(value));
        }
    }
}