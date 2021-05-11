using System.Text.Json.Serialization;

namespace AdventureWorks.Presentation
{
    public interface IKeyJsonConverterProvider
    {
        JsonConverter<TKey> Provide<TKey>();
    }
}