using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdventureWorks.Presentation
{
    public class SalesOrderDetailKeyConverter : JsonConverter<SalesOrderDetailKey>
    {
        public override SalesOrderDetailKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, SalesOrderDetailKey value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}