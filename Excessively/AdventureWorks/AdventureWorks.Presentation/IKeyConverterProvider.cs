using System.Text.Json.Serialization;

namespace AdventureWorks.Presentation
{
    public interface IKeyConverterProvider
    {
        JsonConverter<TKey> Provide<TKey>();
    }
}